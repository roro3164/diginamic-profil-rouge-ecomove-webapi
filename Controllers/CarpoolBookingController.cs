using ecomove_back.Data.Models;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarpoolBookingController : ControllerBase
    {
        private readonly ICarpoolBookingRepository _carpoolBookingRepository;
        private UserManager<AppUser> _userManager;

        public CarpoolBookingController(ICarpoolBookingRepository carpoolBookingRepository, UserManager<AppUser> userManager)
        {
            _carpoolBookingRepository = carpoolBookingRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// Permet de créer une réservation d'une place dans un covoiturage
        /// </summary>
        /// <param name="bookingCreateDTO">DTO contenant les informations de la réservation</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCarpoolBookingAsync(CarpoolBookingCreateDTO bookingCreateDTO)
        {
            var idUserConnect = _userManager.GetUserId(User);

            if (bookingCreateDTO == null)
                return BadRequest("Le DTO de création de réservation ne peut pas être null");

            Response<CarpoolBookingCreateDTO> response = await _carpoolBookingRepository.CreateCarpoolBookingAsync(bookingCreateDTO, idUserConnect);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet d'annuler une réservation d'une place dans un covoiturage
        /// </summary>
        /// <param name="id"> Guid: Id d'un annonce de covoiturage </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelCarpoolBooking(Guid id)
        {
            var idUserConnect = _userManager.GetUserId(User);

            Response<string> response = await _carpoolBookingRepository.CancelCarpoolBookingAsync(id);

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
        public async Task<IActionResult> GetAllCarpoolBookingsByUserIdAsync()
        {
            var idUserConnect = _userManager.GetUserId(User);

            Response<List<CarpoolBooking>> response = await _carpoolBookingRepository.GetAllCarpoolBookingsByUserIdAsync(idUserConnect!);

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
        public async Task<IActionResult> GetFutureCarpoolBookingsByUserIdAsync()
        {
            var idUserConnect = _userManager.GetUserId(User);

            Response<List<CarpoolBooking>> response = await _carpoolBookingRepository.GetFutureCarpoolBookingsByUserIdAsync(idUserConnect!);

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
        [HttpGet]
        public async Task<IActionResult> GetPastCarpoolBookingsByUserIdAsync()
        {
            var idUserConnect = _userManager.GetUserId(User);

            Response<List<CarpoolBooking>> response = await _carpoolBookingRepository.GetAllCarpoolBookingsByUserIdAsync(idUserConnect!);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer un covoiturage en utilisant l'Id de l'annonce 
        /// </summary>
        /// <param name="id"> Guid: Id d'un annonce de covoiturage </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarpoolBookingsByIdAsync(Guid id)
        {
            var idUserConnect = _userManager.GetUserId(User);

            Response<CarpoolBooking> response = await _carpoolBookingRepository.GetCarpoolBookingsByIdAsync(id);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }
    }
}
