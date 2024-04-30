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
    }
}