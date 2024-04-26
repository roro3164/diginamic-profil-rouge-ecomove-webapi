using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.DTOs.BrandDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

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


        public async Task<Response<AppUserDTO>> CreateUserAsync(AppUserDTO userDTO)
        {
            AppUser appUser = new AppUser
            {
                FirstName = Tools.FirstLetterToUpper(userDTO.FirstName),
                LastName = Tools.FirstLetterToUpper(userDTO.LastName),
                PictureProfil = userDTO.PictureProfil,
            };

            await _userManager.SetUserNameAsync(appUser, userDTO.Email.ToLower());
            await _userManager.SetEmailAsync(appUser, userDTO.Email.ToLower());
            await _userManager.AddPasswordAsync(appUser, userDTO.PasswordHash);
            await _userManager.AddToRoleAsync(appUser, "USER");

                await _ecoMoveDbContext.Users.AddAsync(appUser);
                await _ecoMoveDbContext.SaveChangesAsync();

            try
            {

                return new Response<AppUserDTO>
                {
                    Message = $"L'utilisateur {appUser.UserName} a bien été créé",
                    Data = userDTO,
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<AppUserDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }








        }






















        public string TestDate(int newStartDate, int newEndDate)
        {


            // Recuperation de toutes les locations dont la startdate est >= à la nouvelle startdate 

            //RentalVehicle re = new RentalVehicle();

            ////List<RentalVehicle> rentals = _ecoMoveDbContext.RentalVehicles.Where(r => r.VehicleId == re.VehicleId && r.StartDate >= re.StartDate && r.StartDate <= r.EndDate).ToList();
            //List<RentalVehicle> rentals = _ecoMoveDbContext.RentalVehicles.Where(r => r.StartDate >= re.StartDate && r.StartDate <= r.EndDate).ToList();
            int[][] rentalDates = [[2, 5], [8, 10], [20, 23]];

            foreach (var rentalDate in rentalDates)
            {
                if (rentalDate[0] >= newStartDate && rentalDate[0] <= newEndDate && newStartDate > rentalDate[1])
                {
                    return "Pas possible";
                }
            }

            return "Possible";
        }


    }
}
