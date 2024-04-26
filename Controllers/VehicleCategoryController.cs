using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleVehicleCategoryDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VehicleCategoryController : ControllerBase
    {
        private readonly ICategoryRepository _vehicleCategoryRepository;

        public VehicleCategoryController(ICategoryRepository vehicleCategoryRepository)
        {
            _vehicleCategoryRepository = vehicleCategoryRepository;
        }


        /// <summary>
        /// Permet de créer une catégorie de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicleCategory(VehicleCategoryDTO vehicleCategory)
        {
            Response<VehicleCategoryDTO> response = await _vehicleCategoryRepository.CreateVehicleCategoryAsync(vehicleCategory);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }


        /// <summary>
        /// Permet de supprimer une catégorie de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleCategory(int categoryId)
        {
            Response<string> response = await _vehicleCategoryRepository.DeleteVehicleCategoryAsync(categoryId);

            if (response.IsSuccess)
            {
                return Ok(response.Message);
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


        /// <summary>
        /// Permet de mettre à jour une catégorie de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateVehicleById(int categoryId, VehicleCategoryDTO VehicleCategoryDTO)
        {
            Response<VehicleCategoryDTO>? respone = await _vehicleCategoryRepository.UpdateVehicleCategoryAsync(categoryId, VehicleCategoryDTO);

            if (respone.IsSuccess)
                return Ok(respone);
            else if (respone.CodeStatus == 404)
                return NotFound();
            else
                return Problem();
        }


        /// <summary>
        /// Permet de récupérer toutes les catégories de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleCategories()
        {
            Response<List<VehicleCategoryDTO>> response = await _vehicleCategoryRepository.GetAllVehiclesCategoriesAsync();

            if (response.IsSuccess)
                return Ok(response.Data);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        /// <summary>
        /// Permet de récupérer une catégories de véhicule en utilisant son Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVehicleCategoryById(int categoryId)
        {
            Response<VehicleCategoryDTO> response = await _vehicleCategoryRepository.GetVehicleCategoryByIdAsync(categoryId);

            if (response.IsSuccess)
                return Ok(response.Data);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);

        }

    }
}
