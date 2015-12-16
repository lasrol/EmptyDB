using System;
using System.Threading.Tasks;
using EmptyDB.Domain;
using Microsoft.AspNet.Identity;

namespace EmptyDB.Data
{
    public static class Migration
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var email = "test@test.com";
            await userManager.CreateAsync(new User
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = email.ToUpper(),
                CreatedOn = DateTimeOffset.Now,
                Firstname = "Test",
                Lastname = "Testesen"
            }, "Test123!");
        }
    }
}
