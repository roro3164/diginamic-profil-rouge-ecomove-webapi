using ecomove_back.Data.Models;
using ecomove_back.DTOs.MotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleMotorizationController : ControllerBase
    {
        private readonly IMotorizationRepository _motorizationRepository;

        public VehicleMotorizationController(IMotorizationRepository vehiculeMotorizationRepository)
        {
            _motorizationRepository = vehiculeMotorizationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleMotorization(MotorizationDTO vehicleMotorizationDTO)
        {
            Response<MotorizationDTO> response = await _motorizationRepository.CreateMotorizationAsync(vehicleMotorizationDTO);

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
        public async Task<IActionResult> DeleteVehicleMotorization(int id)
        {
            Response<string> response = await _motorizationRepository.DeleteMotorizationAsync(id);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicleMotorizations()
        {
            Response<List<Motorization>> response = await _motorizationRepository.GetAllMotorizationsAsync();

            if (response.IsSuccess)
            {
                return Ok(response.Data);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleMotorizationById(int id)
        {
            Response<int> response = await _motorizationRepository.GetMotorizationByIdAsync(id);
            
            if (response.IsSuccess)
            {
                return Ok(response);
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