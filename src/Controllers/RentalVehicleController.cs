using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.RentalVehicleDTO;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.RentalVehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/RentalVehicles")]
    public class RentalVehicleController(IRentalVehicleService rentalService, UserManager<AppUser> userManager) : ControllerBase
    {
        /// <summary>
        /// Permet de créer une réservation de véhicule
        /// </summary>
        /// <param name="vehicleId">Guid : identifiant du vehicule</param>
        /// <param name="rentalVehicleDTO"></param>
        /// <returns></returns>
        [HttpPost("{vehicleId}")]
        [Authorize(Roles = $"{Roles.USER}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRentalVehicle(Guid vehicleId, RentalVehicleDTO rentalVehicleDTO)
        {
            var idUserConnect = userManager.GetUserId(User);

            Response<bool> createRentalVehicleResult = await rentalService.CreateRentalVehicleAsync(idUserConnect!, vehicleId, rentalVehicleDTO);

            return new JsonResult(createRentalVehicleResult) { StatusCode = createRentalVehicleResult.CodeStatus };
        }


        /// <summary>
        /// Permet d'annuler une réservation de véhicule
        /// </summary>
        /// <param name="rentalId">Guid : identifiant de la réservation</param>
        /// <returns></returns>
        [HttpDelete("{rentalId}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CancelRentalVehicle(Guid rentalId)
        {
            var idUserConnect = userManager.GetUserId(User);

            Response<bool> cancelRentalVehicleResult = await rentalService.CancelRentalVehicleAsync(idUserConnect!, rentalId);

            return new JsonResult(cancelRentalVehicleResult) { StatusCode = cancelRentalVehicleResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer toutes les réservations de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRentalVehicles()
        {
            var idUserConnect = userManager.GetUserId(User);

            Response<List<AllRentalVehicles>> getAllRentalVehiclesResult = await rentalService.GetAllRentalVehiclesAysnc(idUserConnect!);

            return new JsonResult(getAllRentalVehiclesResult) { StatusCode = getAllRentalVehiclesResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer une réservation de véhicule avec son id
        /// </summary>
        /// <param name="rentalId">Guid : identifiant de la réservation</param>
        /// <returns></returns>
        [HttpGet("{rentalId}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRentalVehicleById(Guid rentalId)
        {
            var idUserConnect = userManager.GetUserId(User);

            Response<SingleRentalVehicleDTO> getRentalVehicleByIdResult = await rentalService.GetRentalVehicleByIdAysnc(idUserConnect!, rentalId);

            return new JsonResult(getRentalVehicleByIdResult) { StatusCode = getRentalVehicleByIdResult.CodeStatus };
        }


        /// <summary>
        /// Permet de modifier les dates d'une réservation
        /// </summary>
        /// <param name="rentalId">int : identifiant de la réservation</param>
        /// <param name="rentalVehicleDTO"></param>
        /// <returns></returns>
        [HttpPut("{rentalId}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRentalVehicle(Guid rentalId, RentalVehicleDTO rentalVehicleDTO)
        {
            var idUserConnect = userManager.GetUserId(User);

            Response<bool> updateRentalVehicleResult = await rentalService.UpdateRentalVehicleAsync(idUserConnect!, rentalId, rentalVehicleDTO);

            return new JsonResult(updateRentalVehicleResult) { StatusCode = updateRentalVehicleResult.CodeStatus };
        }
    }
}