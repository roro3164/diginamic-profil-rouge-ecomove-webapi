using ecomove_back.Data.Models;
using ecomove_back.DTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarpoolAnnouncementController : ControllerBase
    {
        private readonly ICarpoolAnnouncementRepository _carpoolAnnouncementRepository;

        public CarpoolAnnouncementController(ICarpoolAnnouncementRepository carpoolAnnouncementRepository)
        {
            _carpoolAnnouncementRepository = carpoolAnnouncementRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCarpoolAnnouncement(CarpoolAnnouncementDTO carpoolAnnouncementDTO)
        {
            Response<CarpoolAnnouncement> response = await _carpoolAnnouncementRepository.CreateCarpoolAnnouncement(carpoolAnnouncementDTO);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarpoolAnnouncements()
        {
            Response<List<CarpoolAnnouncement>> response = await _carpoolAnnouncementRepository.GetAllCarpoolAnnouncements();

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }



        [HttpGet]
        public async Task<IActionResult> GetCarpoolAnnouncementById(Guid id)
        {
            Response<CarpoolAnnouncement> response = await _carpoolAnnouncementRepository.GetCarpoolAnnouncementById(id);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCarpoolAnnouncement(Guid id)
        {
            Response<CarpoolAnnouncement> response = await _carpoolAnnouncementRepository.DeleteCarpoolAnnouncement(id);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCarpoolAnnouncement(Guid id, CarpoolAnnouncementDTO carpoolAnnouncementDTO)
        {
            Response<CarpoolAnnouncement> response = await _carpoolAnnouncementRepository.UpdateCarpoolAnnouncement(id, carpoolAnnouncementDTO);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


    }
}