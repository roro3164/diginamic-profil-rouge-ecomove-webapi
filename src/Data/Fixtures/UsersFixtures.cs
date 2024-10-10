using Ecomove.Api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecomove.Api.Data.Fixtures
{
    public static class UsersFixtures
    {
        public static void SeedRole(EcoMoveDbContext ecoMoveDb)
        {
            var role = ecoMoveDb.Roles.FirstOrDefault(r => r.Name == "ADMIN");

            if (role == null)
            {
                // Création des deux rôles en BDD
                IdentityRole roleAdmin = new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" };
                IdentityRole roleUser = new IdentityRole { Name = "USER", NormalizedName = "USER" };

                ecoMoveDb.Roles.AddRange(roleAdmin, roleUser);
                ecoMoveDb.SaveChanges();
            }
        }

        public static void SeedAdminUser(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@ecomove.com").Result == null)
            {
                var user = new AppUser
                {
                    Id = "d44f7b93-a414-4e2a-b7cb-a47241d41048",
                    UserName = "admin@ecomove.com",
                    Email = "admin@ecomove.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PictureProfil = "https://www.missnumerique.com/blog/wp-content/uploads/reussir-sa-photo-de-profil-michael-dam.jpg",
                };

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
                var user = new AppUser
                {
                    Id = "4d000b70-12b0-4429-b3b1-56ff0c7f2531",
                    UserName = "user@ecomove.com",
                    Email = "user@ecomove.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                };

                IdentityResult result = userManager.CreateAsync(user, "Azerty1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.USER).Wait();
                }
            }

            if (userManager.FindByEmailAsync("user2@ecomove.com").Result == null)
            {
                var user = new AppUser
                {
                    Id = "99040726-f045-4702-b598-3855b0f0f43a",
                    UserName = "user2@ecomove.com",
                    Email = "user2@ecomove.com",
                    FirstName = "Toto",
                    LastName = "Lipo",
                };

                IdentityResult result = userManager.CreateAsync(user, "Azerty1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.USER).Wait();
                }
            }
        }
    }
}
