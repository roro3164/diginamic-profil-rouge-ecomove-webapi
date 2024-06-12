using Ecomove.Api.Data;
using Ecomove.Api.DTOs.VehicleDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Permet de créer un véhicule
        /// </summary>
        [HttpPost]
        [Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> CreateVehicleAsync(VehicleForCreateDTO vehicleForCreationDTO)
        {
            Response<VehicleForCreateDTO> response = await _vehicleRepository.CreateVehicleAsync(vehicleForCreationDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return new JsonResult(response) { StatusCode = response.CodeStatus };
            }
        }

        /// <summary>
        /// Permet de récupérer tous les véhicules
        /// </summary>
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllVehiclesAsync()
        {
            Response<List<VehicleForGetDTO>> response = await _vehicleRepository.GetAllVehiclesAsync();

            if (response.IsSuccess)
                return Ok(response.Data);
            else
                return new JsonResult(response) { StatusCode = response.CodeStatus };
        }

        /// <summary>
        /// Permet de trouver un véhicule avec un ID
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetVehicleById(Guid id)
        {
            Response<VehicleForGetDTO> response = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return new JsonResult(response) { StatusCode = response.CodeStatus};
            }
        }

        /// <summary>
        /// Permet de trouver un véhicule avec un ID pour un admin
        /// </summary>
        [HttpGet("admin/{id}")]
        //[Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> GetVehicleByIdForAdmin(Guid id)
        {
            Response<VehicleForGetByIdForAdminDTO> response = await _vehicleRepository.GetVehicleByIdForAdminAsync(id);
            if (response.IsSuccess)
                return Ok(response.Data);
            else
                return new JsonResult(response) { StatusCode = response.CodeStatus };
        }

        /// <summary>
        /// Permet de supprimer un véhicule
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            Response<bool> response = await _vehicleRepository.DeleteVehicleAsync(id);
            if (response.IsSuccess)
                return Ok(response);
            else
                return new JsonResult(response) { StatusCode = response.CodeStatus };
        }

        /// <summary>
        /// Permet de mettre à jour un véhicule
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> UpdateVehicle(Guid id, VehicleForUpdateDTO vehicleDto)
        {
            Response<VehicleForUpdateDTO> response = await _vehicleRepository.UpdateVehicleAsync(id, vehicleDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return new JsonResult(response) { StatusCode = response.CodeStatus };
            }
        }

        /// <summary>
        /// Permet de changer le statut d'un véhicule
        /// </summary>
        [HttpPut("status/{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> ChangeVehicleStatus(Guid id, VehicleForChangeStatusDTO statusDto)
        {
            Response<VehicleForChangeStatusDTO> response = await _vehicleRepository.ChangeVehicleStatusAsync(id, statusDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return new JsonResult(response) { StatusCode = response.CodeStatus };
            }
        }
    }
}
