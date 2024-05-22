using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AppUserRepository(
            EcoMoveDbContext ecoMoveDbContext,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _ecoMoveDbContext = ecoMoveDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task<Response<string>> CreateUserAsync(CreateAppUserDTO userDTO)
        {
            IdentityRole? roleUser = await _roleManager.FindByNameAsync("USER");

            if (roleUser == null)
            {
                return new Response<string>
                {
                    Message = "Le rôle n'existe pas",
                    IsSuccess = false,
                    CodeStatus = 404
                };
            }

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

            IdentityResult result = await _userManager.CreateAsync(appUser, userDTO.Password);

            if (result.Succeeded)
            {
                // Voir si on se sert de la table intermediaire [AspNetUserRoles], sinon supprimer cette ligne
                await _userManager.AddToRoleAsync(appUser, "USER");

                return new Response<string>
                {
                    Message = $"L'utilisateur {appUser.Email} a bien été crée",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            else
            {
                return new Response<string>
                {
                    Message = (string.Join(" | ", result.Errors.Select(e => e.Description))),
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }


        // Uniquement pour l'amdin, voir les infos à renvoyer
        public async Task<Response<List<AllUsersDTO>>> GetAllUsersAysnc()
        {
            try
            {
                // Faut il récupérer aussi l'admin ???
                List<AppUser> AppUsers = await _ecoMoveDbContext.Users.ToListAsync();

                if (AppUsers.Count == 0)
                {
                    return new Response<List<AllUsersDTO>>
                    {
                        Message = "Aucune utilisateur trouvé",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                List<AllUsersDTO> AppUsersDTO = new List<AllUsersDTO>();

                foreach (AppUser appUser in AppUsers)
                {
                    AppUsersDTO.Add(new AllUsersDTO
                    {
                        Id = appUser.Id,
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        PictureProfil = appUser.PictureProfil,
                        Email = appUser.Email
                    });
                }

                return new Response<List<AllUsersDTO>>
                {
                    Data = AppUsersDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<List<AllUsersDTO>>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }


        // Uniquement pour l'amdin, voir les infos à renvoyer 
        public async Task<Response<UserDTO>> GetUserByIdAysnc(string userId)
        {
            try
            {
                AppUser? appUser = await _ecoMoveDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (appUser == null)
                {
                    return new Response<UserDTO>
                    {
                        Message = "L'utilisateur que vous voulez n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                UserDTO UserDTO = new UserDTO
                {
                    Id = appUser.Id,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    PictureProfil = appUser.PictureProfil,
                    Email = appUser.Email,
                };

                return new Response<UserDTO>
                {
                    Data = UserDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<UserDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<UpdateUserDTO>> UpdateUserAysnc(string userId, UpdateUserDTO userDTO)
        {
            try
            {
                AppUser? appUser = await _ecoMoveDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (appUser == null)
                {
                    return new Response<UpdateUserDTO>
                    {
                        Message = "L'utilisateur n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                if (userDTO.Email != null)
                {
                    var resultEmail = await _userManager.SetEmailAsync(appUser, userDTO.Email);

                    if (resultEmail.Succeeded)
                    {
                        await _userManager.SetUserNameAsync(appUser, userDTO.Email);
                    }
                    else
                    {
                        return new Response<UpdateUserDTO>
                        {
                            Message = string.Join(" | ", resultEmail.Errors.Select(e => e.Description)),
                            IsSuccess = false,
                            CodeStatus = 500
                        };
                    }
                }

                if (userDTO.CurrentPassword != null && userDTO.NewPassword != null)
                {
                    var resultPassword = await _userManager.ChangePasswordAsync(appUser, userDTO.CurrentPassword, userDTO.NewPassword);
                    if (!resultPassword.Succeeded)
                    {
                        return new Response<UpdateUserDTO>
                        {
                            Message = string.Join(" | ", resultPassword.Errors.Select(e => e.Description)),
                            IsSuccess = false,
                            CodeStatus = 500
                        };
                    }
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

                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<UpdateUserDTO>
                {
                    Message = "L'utilisateur a bien été modifié",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<UpdateUserDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }


        /* Faire les vérification avant de supprimer
         * Verifier que le user n'a pas de réservation en cours
         * Voir si on supprime les reservation et les covoiturages ?????
        */
        public async Task<Response<string>> DeleteUserAsync(string userId)
        {
            AppUser? appUser = await _ecoMoveDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (appUser == null)
            {
                return new Response<string>
                {
                    Message = "L'utilisateur n'existe pas",
                    IsSuccess = false,
                    CodeStatus = 404
                };
            }

            var result = await _userManager.DeleteAsync(appUser);

            if (result.Succeeded)
            {
                return new Response<string>
                {
                    Message = "L'utilisateur a bien été supprimé",
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            else
            {
                return new Response<string>
                {
                    Message = string.Join(" | ", result.Errors.Select(e => e.Description)),
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }



    }
}
