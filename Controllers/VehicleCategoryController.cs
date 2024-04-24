using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleCategoryDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleCategoryController : ControllerBase
    {
        private readonly IVehicleCategoryRepository _vehicleCategoryRepository;

        public VehicleCategoryController(IVehicleCategoryRepository vehicleCategoryRepository)
        {
            _vehicleCategoryRepository = vehicleCategoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleCategory(VehicleCategoryForCreationDTO vehicleCategory)
        {
            Response<VehicleCategoryForCreationDTO> response = await _vehicleCategoryRepository.CreateVehicleCategoryAsync(vehicleCategory);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return Problem(response.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleCategory(int vehicleCategoryId)
        {
            Response<string> response = await _vehicleCategoryRepository.DeleteVehicleCategoryAsync(vehicleCategoryId);

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