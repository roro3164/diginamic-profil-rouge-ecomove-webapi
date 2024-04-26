using ecomove_back.Data.Models;
using ecomove_back.DTOs.MotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MotorizationController : ControllerBase
    {
        private readonly IMotorizationRepository _motorizationRepository;

        public MotorizationController(IMotorizationRepository motorizationRepository)
        {
            _motorizationRepository = motorizationRepository;
        }

        /// <summary>
        /// Permet de créer une motorisation de véhicule
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> CreateVehicleMotorization(MotorizationDTO motorizationDTO)
        {
            Response<MotorizationDTO> response = await _motorizationRepository.CreateMotorizationAsync(motorizationDTO);

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
        /// Permet de supprimer une motorisation de véhicule
        /// </summary>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorization(int id)
        {
            Response<string> response = await _motorizationRepository.DeleteMotorizationAsync(id);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer toutes les motorisations de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMotorizations()
        {
            Response<List<Motorization>> response = await _motorizationRepository.GetAllMotorizationsAsync();

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

        /// <summary>
        /// Permet de récupérer une motorisation de véhicule en utilisant son Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorizationById(int id)
        {
            Response<MotorizationDTO> response = await _motorizationRepository.GetMotorizationByIdAsync(id);
            
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

        /// <summary>
        /// Permet de modifier une Motorisation
        /// </summary>
        /// <param name="id">int : identifiant de la motorisation</param>
        /// <param name="motorizationDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMotorizationById(int id, MotorizationDTO motorizationDTO)
        {
            Response<MotorizationDTO> response = await _motorizationRepository.UpdateMotorizationByIdAsync(id, motorizationDTO);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }
    }      
}



