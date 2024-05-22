using Ecomove.Api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecomove.Api.Data.Fixtures
{
    public static class UsersFixtures
    {
        public static void SeedAdminUser(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@ecomove.com").Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = "admin@ecomove.com";
                user.Email = "admin@ecomove.com";
                user.FirstName = "John";
                user.LastName = "Doe";

                IdentityResult result = userManager.CreateAsync(user, "Azerty1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.ADMIN).Wait();
                }
            }
        }


        public static void SeedUser(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("user@ecomove.com").Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = "user@ecomove.com";
                user.Email = "user@ecomove.com";
                user.FirstName = "Jane";
                user.LastName = "Doe";
                user.PictureProfil = "https://www.missnumerique.com/blog/wp-content/uploads/reussir-sa-photo-de-profil-michael-dam.jpg";

                IdentityResult result = userManager.CreateAsync(user, "Azerty1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.USER).Wait();
                }
            }

            if (userManager.FindByEmailAsync("user2@ecomove.com").Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = "user2@ecomove.com";
                user.Email = "user2@ecomove.com";
                user.FirstName = "Toto";
                user.LastName = "Lipo";

                IdentityResult result = userManager.CreateAsync(user, "Azerty1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.USER).Wait();
                }
            }
        }



    }
}
