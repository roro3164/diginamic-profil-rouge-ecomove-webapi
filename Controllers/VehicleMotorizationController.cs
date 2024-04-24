using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleMotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleMotorizationController : ControllerBase
    {
        private readonly IVehicleMotorizationRepository _vehiculeMotorizationRepository;

        public VehicleMotorizationController(IVehicleMotorizationRepository vehiculeMotorizationRepository)
        {
            _vehiculeMotorizationRepository = vehiculeMotorizationRepository;
        }

        [HttpPost]
        public async Task <IActionResult> CreateVehicleMotorization(VehicleMotorizationForCreationDTO vehicleMotorizationDTO)
        { 
            Response<VehicleMotorizationForCreationDTO> response = await _vehiculeMotorizationRepository.CreateVehicleMotorizationAsync(vehicleMotorizationDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            else
            {
                return Problem(response.Message); 
            }

        }
    }
}