using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class AppUserRepository(
        EcoMoveDbContext ecoMoveDbContext, 
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager
        ) : IAppUserRepository
    {
        public async Task<ErrorOr<Created>> CreateUserAsync(CreateAppUserDTO userDTO)
        {
            IdentityRole? roleUser = await roleManager.FindByNameAsync("USER");

            try
            {
                if (roleUser is null) return Error.NotFound(description: $"Le rôle n'existe pas.");

                AppUser appUser = new AppUser
                {
                    UserName = userDTO.Email,
                    Email = userDTO.Email,
                    NormalizedEmail = userDTO.Email.ToUpperInvariant(),
                    FirstName = Tools.FirstLetterToUpper(userDTO.FirstName),
                    LastName = Tools.FirstLetterToUpper(userDTO.LastName),
                    PictureProfil = userDTO?.PictureProfil?.ToLower(),
                    RoleId = roleUser.Id,
                    Role = roleUser
                };

                IdentityResult result = await userManager.CreateAsync(appUser, userDTO.Password);

                if (result.Succeeded)
                {
                    // Voir si on se sert de la table intermediaire [AspNetUserRoles], sinon supprimer cette ligne
                    await userManager.AddToRoleAsync(appUser, "USER");

                    return Result.Created;
                }
                else
                {
                    return Error.Unexpected(description: $"Une erreur est survenue lors de la création de l'utilisateur");
                }
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        // Uniquement pour l'amdin, voir les infos à renvoyer
        public async Task<ErrorOr<List<AppUser>>> GetAllUsersAysnc()
        {
            try
            {
                return await ecoMoveDbContext.Users.ToListAsync();
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        public async Task<ErrorOr<AppUser>> GetUserByIdAysnc(string userId)
        {
            try
            {
                AppUser? appUser = await ecoMoveDbContext.Users.FindAsync(userId);

                if (appUser is null) return Error.NotFound(description: $"User with ID {userId} is not found.");

                return appUser;
            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }
        }

        public async Task<ErrorOr<Updated>> UpdateUserAysnc(string userId, UpdateUserDTO userDTO)
        {
            try
            {
                AppUser? appUser = await ecoMoveDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (appUser is null) return Error.NotFound(description: $"User with ID {userId} not found.");

                if (userDTO.Email != null)
                {
                    var resultEmail = await userManager.SetEmailAsync(appUser, userDTO.Email);

                    if (resultEmail.Succeeded)
                    {
                        await userManager.SetUserNameAsync(appUser, userDTO.Email);
                    }
                    else
                    {
                        return Error.NotFound(description: $"User email with ID {userId} is empty.");
                    }
                }

                if (userDTO.CurrentPassword != null && userDTO.NewPassword != null)
                {
                    var resultPassword = await userManager.ChangePasswordAsync(appUser, userDTO.CurrentPassword, userDTO.NewPassword);

                    if (!resultPassword.Succeeded) return Error.Unexpected(description: string.Join(" | ", resultPassword.Errors.Select(e => e.Description)));
                }

                if (userDTO.FirstName != null)
                {
                    appUser.FirstName = userDTO.FirstName;
                }

                if (userDTO.LastName != null)
                {
                    appUser.LastName = userDTO.LastName;
                }

                if (userDTO.PictureProfil != null)
                {
                    appUser.PictureProfil = userDTO.PictureProfil;
                }

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Updated;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }



        /* Faire les vérification avant de supprimer
         * Verifier que le user n'a pas de réservation en cours
         * Voir si on supprime les reservation et les covoiturages ?????
        */
        public async Task<ErrorOr<Deleted>> DeleteUserAsync(string id)
        {
            try
            {
                AppUser? appUser = await ecoMoveDbContext.Users.FindAsync(id);

                if (appUser is null) return Error.NotFound(description: $"User with ID {id} not found.");

                ecoMoveDbContext.Users.Remove(appUser);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
    }
}
