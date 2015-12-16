using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using EmptyDB.Domain;
using EmptyDB.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EmptyDB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult Login(string returnPath)
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnPath)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<IActionResult>(View(loginModel));

            var user = await _userManager.FindByEmailAsync(loginModel.Username);
            if(user == null) { ModelState.AddModelError("", "Wrong username and/or password"); return View(loginModel); }

            var result =
                await
                    _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);

            if (result.Succeeded)
            {
                if (returnPath == null)
                {
                    return Redirect(Url.Action("Index", "Home"));
                }
                return Redirect(returnPath);
            }

            ModelState.AddModelError("", "Wrong username and/or password");
            return View(loginModel);
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public Task<IActionResult> ResetPassword(string code, string userId)
        {
            ViewBag.UserID = userId;
            ViewBag.Code = code;

            return Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.UserID))
            {
                return View("PasswordResetError");
            }
            var token = model.Code;
            var userid = model.UserID;
            var user = await _userManager.FindByIdAsync(userid);
            var result = await _userManager.ResetPasswordAsync(user, token, userid);

            if (!result.Succeeded)
            {
                ViewBag.Message = result.Errors.First().Description;
                return View("PasswordResetError");
            }

            return View("PasswordResetSuccess");
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                ViewBag.Link = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                //Should EMAIL Link, showing on page for testing purposes
                return View("PasswordResetMail");
            }

            ViewBag.Message = "Unknown email";
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
