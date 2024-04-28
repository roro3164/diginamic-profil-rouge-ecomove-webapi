using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AppUserController(
            IAppUserRepository appUserRepository,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        /// <summary>
        /// Permet de créer un utilisateur avec le rôle USER
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser(AppUserDTO userDTO)
        {
            Response<string> response = await _appUserRepository.CreateUserAsync(userDTO);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }












        // NE PAS SUPPRIMER !!!
        //[HttpPost]
        //public IActionResult TestDate(int newStartDate, int newEndDate)
        //{
        //    string response = _appUserRepository.TestDate(newStartDate, newEndDate);

        //    return Ok(response);
        //}

    }
}
