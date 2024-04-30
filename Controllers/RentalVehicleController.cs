using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.DTOs.RentalVehicleDTO;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using ecomove_back.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RentalVehicleController : ControllerBase
    {
        private IRentalVehicleRepository _rentalVehicleRepository;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RentalVehicleController(
            IRentalVehicleRepository rentalVehicleRepository,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _rentalVehicleRepository = rentalVehicleRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Permet de créer une réservation de véhicule
        /// </summary>
        /// <param name="vehicleId">Guid : identifiant du vehicule</param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost("{vehicleId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRentalVehicle(Guid vehicleId, RentalVehicleDTO userDTO)
        {
            string userId = "26554434-3066-443c-ac97-0119cccd215f";

            Response<string> response = await _rentalVehicleRepository.CreateRentalVehicleAsync(userId, vehicleId, userDTO);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }


        /// <summary>
        /// Permet de modifier les dates d'une réservation
        /// </summary>
        /// <param name="rentalId">int ; identifiant de la réservation</param>
        /// <param name="rentalVehicleDTO"></param>
        /// <returns></returns>
        [HttpPut("{rentalId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRentalVehicle(int rentalId, RentalVehicleDTO rentalVehicleDTO)
        {
            Response<RentalVehicleDTO> response = await _rentalVehicleRepository.UpdateRentalVehicleAsync(rentalId, rentalVehicleDTO);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

    }
}