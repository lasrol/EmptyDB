using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;

namespace EmptyDB.Config
{
    public class IdConfig
    {
        public static void Get(IdentityOptions o)
        {
            o.User.RequireUniqueEmail = true;
            //Password
            o.Password.RequiredLength = 7;
            o.Password.RequireUppercase = false;
            o.Password.RequireLowercase = false;

            o.SignIn.RequireConfirmedEmail = false;
            o.Cookies.ApplicationCookieAuthenticationScheme = "ApplicationCookie";
            o.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/Account/Login");
            o.Cookies.ApplicationCookie.LoginPath = new PathString("/Account/Login");
            o.Cookies.ApplicationCookie.LogoutPath = new PathString("/");
            o.Cookies.ApplicationCookie.AuthenticationScheme =
            IdentityCookieOptions.ApplicationCookieAuthenticationType = "ApplicationCookie";
            o.Cookies.ApplicationCookie.AutomaticChallenge = true;
        }
    }
}