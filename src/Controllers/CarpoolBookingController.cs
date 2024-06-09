using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CarpoolBookingDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.CarpoolBookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ErrorOr;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/carpoolbookings")]
    // [Authorize(Roles = $"{Roles.ADMIN}")]
    public class CarpoolBookingController(ICarpoolBookingService carpoolBookingService) : ControllerBase
    {
        /// <summary>
        /// Permet de créer une réservation d'une place dans un covoiturage
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> CreateCarpoolBooking(CarpoolBookingDTO bookingCreateDTO)
        {
            var appUserId = User.Identity?.Name;

            if (bookingCreateDTO == null)
            {
                return BadRequest("Le DTO de création de réservation ne peut pas être null");
            }

            Response<bool> createCarpoolBookingResult = await carpoolBookingService.CreateCarpoolBookingAsync(bookingCreateDTO, appUserId!);

            return new JsonResult(createCarpoolBookingResult) { StatusCode = createCarpoolBookingResult.CodeStatus };
        }

        /// <summary>
        /// Permet d'annuler une réservation d'une place dans un covoiturage
        /// </summary>
        /// <param name="carpoolAnnouncementId"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        [HttpDelete("{carpoolAnnouncementId:guid}/{appUserId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CancelCarpoolBooking(Guid carpoolAnnouncementId, string appUserId)
        {
            Response<bool> cancelCarpoolBookingResult = await carpoolBookingService.CancelCarpoolBookingAsync(carpoolAnnouncementId, appUserId);

            return new JsonResult(cancelCarpoolBookingResult) { StatusCode = cancelCarpoolBookingResult.CodeStatus };
        }

        /// <summary>
        /// Permet de récupérer toutes les réservations de covoiturage d'un utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCarpoolBookingsByUserId()
        {
            var appUserId = User.Identity?.Name;

            Response<List<CarpoolBookingDTO>> getAllCarpoolBookingsResult = await carpoolBookingService.GetAllCarpoolBookingsByUserIdAsync(appUserId!);

            return new JsonResult(getAllCarpoolBookingsResult) { StatusCode = getAllCarpoolBookingsResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer toutes les réservations de covoiturage futures d'un utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet("future")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFutureCarpoolBookingsByUserId()
        {
            var appUserId = User.Identity?.Name; // Obtiene el ID del usuario autenticado

            Response<List<CarpoolBookingDTO>> getFutureCarpoolBookingsResult = await carpoolBookingService.GetFutureCarpoolBookingsByUserIdAsync(appUserId!);

            return new JsonResult(getFutureCarpoolBookingsResult) { StatusCode = getFutureCarpoolBookingsResult.CodeStatus };
        }

        /// <summary>
        /// Permet de récupérer toutes les réservations de covoiturage passées d'un utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet("past")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPastCarpoolBookingsByUserId()
        {
            var appUserId = User.Identity?.Name; // Obtiene el ID del usuario autenticado

            Response<List<CarpoolBookingDTO>> getPastCarpoolBookingsResult = await carpoolBookingService.GetPastCarpoolBookingsByUserIdAsync(appUserId!);

            return new JsonResult(getPastCarpoolBookingsResult) { StatusCode = getPastCarpoolBookingsResult.CodeStatus };
        }

        /// <summary>
        /// Permet de récupérer une réservation de covoiturage par ID d'annonce et ID d'utilisateur
        /// </summary>
        /// <param name="carpoolAnnouncementId"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        [HttpGet("{carpoolAnnouncementId:guid}/{appUserId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCarpoolBookingByAnnouncementId(Guid carpoolAnnouncementId, string appUserId)
        {
            Response<CarpoolBookingDTO> getCarpoolBookingResult = await carpoolBookingService.GetCarpoolBookingByAnnouncementIdAsync(carpoolAnnouncementId, appUserId);

            return new JsonResult(getCarpoolBookingResult) { StatusCode = getCarpoolBookingResult.CodeStatus };
        }
    }
}
