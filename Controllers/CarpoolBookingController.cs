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
            {
                return BadRequest("Le DTO de création de réservation ne peut pas être null");
            }

            Response<CarpoolBookingCreateDTO> response = await _carpoolBookingRepository.CreateCarpoolBookingAsync(bookingCreateDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return Problem(response.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelCarpoolBooking(int id)
        {
            Response<string> response = await _carpoolBookingRepository.CancelCarpoolBookingAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response.Message);
            }
            else if (response.CodeStatus == 404)
            {
                return NotFound(response.Message);
            }
            else
            {
                return Problem(response.Message);
            }
        }
    }
}
 