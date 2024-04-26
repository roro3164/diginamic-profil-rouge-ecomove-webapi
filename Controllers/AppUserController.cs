using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.DTOs.BrandDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http;
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




        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAppUser(AppUserDTO appUserDTO)
        {
            Response<AppUserDTO> response = await _appUserRepository.CreateUserAsync(appUserDTO);

            return Ok(response);
        }



        [HttpPost]
        public IActionResult TestDate(int newStartDate, int newEndDate)
        {
            string response = _appUserRepository.TestDate(newStartDate, newEndDate);

            return Ok(response);
        }


    }
}
