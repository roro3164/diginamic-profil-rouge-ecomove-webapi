using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleMotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using ecomove_back.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleMotorizationController : ControllerBase
    {
        private readonly IVehicleMotorizationRepository _vehicleMotorizationRepository; 

        public VehicleMotorizationController(IVehicleMotorizationRepository vehiculeMotorizationRepository)
        {
            _vehicleMotorizationRepository = vehiculeMotorizationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleMotorization(VehicleMotorizationForCreationDTO vehicleMotorizationDTO)
        {
            Response<VehicleMotorizationForCreationDTO> response = await _vehicleMotorizationRepository.CreateVehicleMotorizationAsync(vehicleMotorizationDTO);

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
            Response<string> response = await _vehicleMotorizationRepository.DeleteVehicleMotorizationAsync(id);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }
    }
}