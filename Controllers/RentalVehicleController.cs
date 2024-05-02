using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.RentalVehicleDTO;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = $"{Roles.USER}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRentalVehicle(Guid vehicleId, RentalVehicleDTO userDTO)
        {
            var idUserConnect = _userManager.GetUserId(User);

            Response<string> response = await _rentalVehicleRepository.CreateRentalVehicleAsync(idUserConnect!, vehicleId, userDTO);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de modifier les dates d'une réservation
        /// </summary>
        /// <param name="rentalId">int : identifiant de la réservation</param>
        /// <param name="rentalVehicleDTO"></param>
        /// <returns></returns>
        [HttpPut("{rentalId}")]
        [Authorize(Roles = $"{Roles.USER}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRentalVehicle(Guid rentalId, RentalVehicleDTO rentalVehicleDTO)
        {



            Response<RentalVehicleDTO> response = await _rentalVehicleRepository.UpdateRentalVehicleAsync(rentalId, rentalVehicleDTO);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet d'annuler une réservation de véhicule
        /// </summary>
        /// <param name="rentalId">Guid : identifiant de la réservation</param>
        /// <returns></returns>
        [HttpPut("{rentalId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CancelRentalVehicle(Guid rentalId)
        {
            Response<string> response = await _rentalVehicleRepository.CancelRentalVehicleAsync(rentalId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer toutes les réservations de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRentalVehicles()
        {
            Response<List<AllRentalVehicles>> response = await _rentalVehicleRepository.GetAllRentalVehiclesAysnc();

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer une réservation de véhicule avec son id
        /// </summary>
        /// <param name="rentalId">Guid : identifiant de la réservation</param>
        /// <returns></returns>
        [HttpGet("{rentalId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRentalVehicleById(Guid rentalId)
        {
            Response<SingleRentalVehicleDTO> response = await _rentalVehicleRepository.GetRentalVehicleByIdAysnc(rentalId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }



    }
}