using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleBrandDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using ecomove_back.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleBrandController : ControllerBase
    {
        private readonly IVehicleBrandRepository _vehicleBrandRepository;

        public VehicleBrandController(IVehicleBrandRepository vehicleBrandRepository)
        {
            _vehicleBrandRepository = vehicleBrandRepository;
        }


        [HttpPost]
        public async Task<ActionResult> CreateVehicleBrand(VehicleBrandForCreationDTO vehicleBrandDTO)
        {
            Response<VehicleBrandForCreationDTO> response = await _vehicleBrandRepository.CreateBrandVehicleAsync(vehicleBrandDTO);

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