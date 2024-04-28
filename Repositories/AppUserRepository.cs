using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace ecomove_back.Repositories
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


        public async Task<Response<string>> CreateUserAsync(AppUserDTO userDTO)
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








    }
}
