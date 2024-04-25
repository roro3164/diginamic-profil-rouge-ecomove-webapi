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

        /// <summary>
        /// Permet de créer une marque
        /// </summary>
        /// <param name="vehicleBrandDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateVehicleBrand(VehicleBrandDTO vehicleBrandDTO)
        {
            Response<VehicleBrandDTO> response = await _vehicleBrandRepository.CreateBrandVehicleAsync(vehicleBrandDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return Problem(response.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer une marque
        /// </summary>
        /// <param name="id">int : identifiant de la marque</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteVehicleBrand(int id)
        {
            Response<string> response = await _vehicleBrandRepository.DeleteBrandVehicleAsync(id);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer la liste des marques de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllVehiclesBrand()
        {
            Response<List<VehicleBrandDTO>> response = await _vehicleBrandRepository.GetAllBrandAysnc();

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        /// <summary>
        /// Permet de récuperer une marque avec son id
        /// </summary>
        /// <param name="id">int : identifiant de la marque</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVehiclesBrandById(int id)
        {
            Response<VehicleBrandDTO> response = await _vehicleBrandRepository.GetBrandByIdAysnc(id);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de modifier une Marque
        /// </summary>
        /// <param name="id">int : identifiant de la marque</param>
        /// <param name="brandDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehiclesBrandById(int id, VehicleBrandDTO brandDto)
        {
            Response<VehicleBrandDTO> response = await _vehicleBrandRepository.UpdateBrandAysnc(id, brandDto);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }




    }
}