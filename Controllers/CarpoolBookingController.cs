using ecomove_back.Data.Models;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarpoolBookingController : ControllerBase
    {
        private readonly ICarpoolBookingRepository _carpoolBookingRepository;

        public CarpoolBookingController(ICarpoolBookingRepository carpoolBookingRepository)
        {
            _carpoolBookingRepository = carpoolBookingRepository;
        }

        /// <summary>
        /// Permet de créer une réservation d'une place dans un covoiturage
        /// </summary>
        /// <param name="bookingCreateDTO">DTO contenant les informations de la réservation</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCarpoolBookingAsync(CarpoolBookingCreateDTO bookingCreateDTO)
        {
            if (bookingCreateDTO == null)
                return BadRequest("Le DTO de création de réservation ne peut pas être null");

            Response<CarpoolBookingCreateDTO> response = await _carpoolBookingRepository.CreateCarpoolBookingAsync(bookingCreateDTO);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet d'annuler une réservation d'une place dans un covoiturage
        /// </summary>
        /// <returns>ActionResult</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelCarpoolBooking(Guid carpoolAnnouncementId, string appUserId)

        {
            Response<string> response = await _carpoolBookingRepository.CancelCarpoolBookingAsync(carpoolAnnouncementId, appUserId);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer toutes les covoiturages d'un collaborateur
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllCarpoolBookingsByUserIdAsync(string userId)

        {
            Response<List<CarpoolBooking>> response = await _carpoolBookingRepository.GetAllCarpoolBookingsByUserIdAsync(userId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer toutes les covoiturages futurs d'un collaborateur
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetFutureCarpoolBookingsByUserIdAsync(string userId)

        {
            Response<List<CarpoolBooking>> response = await _carpoolBookingRepository.GetFutureCarpoolBookingsByUserIdAsync(userId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer toutes les covoiturages passés d'un collaborateur
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetPastCarpoolBookingsByUserIdAsync(string userId)
   
        {
            Response<List<CarpoolBooking>> response = await _carpoolBookingRepository.GetAllCarpoolBookingsByUserIdAsync(userId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer un covoiturage en utilisant son l'Id de l'annonce et de le collaborateur
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarpoolBookingsByIdAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            Response<CarpoolBooking> response = await _carpoolBookingRepository.GetCarpoolBookingsByIdAsync(carpoolAnnouncementId, appUserId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }
    }
}
 