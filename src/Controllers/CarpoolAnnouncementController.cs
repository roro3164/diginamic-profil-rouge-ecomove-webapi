using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CapoolAnnouncementDTOs;
using Ecomove.Api.DTOs.CarpoolAnnouncementDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarpoolAnnouncementController : ControllerBase
    {
        private readonly ICarpoolAnnouncementRepository _carpoolAnnouncementRepository;
        private readonly UserManager<AppUser> _userManager;

        public CarpoolAnnouncementController(ICarpoolAnnouncementRepository carpoolAnnouncementRepository, UserManager<AppUser> userManager)
        {
            _carpoolAnnouncementRepository = carpoolAnnouncementRepository;
            _userManager = userManager;
        }


        [HttpPost]
        [Authorize(Roles = $"{Roles.USER}")]
        public async Task<IActionResult> CreateCarpoolAnnouncement(CarpoolAnnouncementInGoingDTO carpoolAnnouncementDTO)
        {
            // Get the connected user ID
            string connectedUserId = _userManager.GetUserId(User)!;

            Response<CarpoolAnnouncement>? response = await _carpoolAnnouncementRepository.CreateCarpoolAnnouncement(connectedUserId, carpoolAnnouncementDTO);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCarpoolAnnouncementById(Guid id)
        {
            Response<CarpoolAnnouncementOutGoingDTO>? response = await _carpoolAnnouncementRepository.GetCarpoolAnnouncementById(id);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCarpoolAnnouncements()
        {
            Response<List<CarpoolAnnouncementOutGoingDTO>>? response = await _carpoolAnnouncementRepository.GetAllCarpoolAnnouncements();

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCarpoolAnnouncement(Guid id, CarpoolAnnouncementDTO carpoolAnnouncementDTO)
        {
            // Get the connected user ID
            string connectedUserId = _userManager.GetUserId(User)!;

            Response<CarpoolAnnouncement>? response = await _carpoolAnnouncementRepository.UpdateCarpoolAnnouncement(id, connectedUserId, carpoolAnnouncementDTO);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCarpoolAnnouncement(Guid id)
        {
            // Get the connected user ID
            string connectedUserId = _userManager.GetUserId(User)!;

            Response<CarpoolAnnouncement>? response = await _carpoolAnnouncementRepository.DeleteCarpoolAnnouncement(id, connectedUserId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }
    }
}