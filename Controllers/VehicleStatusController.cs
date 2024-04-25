using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.DTOs.VehicleStatusDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using ecomove_back.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleStatusController : ControllerBase
    {
        private readonly IVehicleStatusRepository _vehicleStatusRepository;

        public VehicleStatusController(IVehicleStatusRepository vehicleStatusRepository)
        {
            _vehicleStatusRepository = vehicleStatusRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleStatus(VehicleStatusDTO vehicleStatusForCreationDTO)
        {
            Response<VehicleStatusDTO> response = await _vehicleStatusRepository.CreateVehicleStatusAsync(vehicleStatusForCreationDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return Problem(response.Message);
            }
        }


        [HttpDelete("{vehicleStatusId}")]
        public async Task<IActionResult> DeleteVehicleStatus(int vehicleStatusId)
        {
            Response<string> response = await _vehicleStatusRepository.DeleteVehicleStatusAsync(vehicleStatusId);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicleStatusAsync()
        {
            
            Response<List<VehicleStatusDTO>> response = await _vehicleStatusRepository.GetAllVehicleStatusAsync();

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);

        }

        [HttpGet("{vehicleStatusId}")]
        public async Task<IActionResult> GetByIdVehicleStatusAsync(int vehicleStatusId)
        {

            Response<VehicleStatusDTO> response = await _vehicleStatusRepository.GetByIdVehicleStatusAsync(vehicleStatusId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);

        }
    }
}