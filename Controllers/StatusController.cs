using ecomove_back.DTOs.StatusDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _StatusRepository;

        public StatusController(IStatusRepository StatusRepository)
        {
            _StatusRepository = StatusRepository;
        }

        /// <summary>
        /// Permet de créer un statut de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateStatus(StatusDTO StatusForCreationDTO)
        {
            Response<StatusDTO> response = await _StatusRepository.CreateStatusAsync(StatusForCreationDTO);

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
        /// Permet de supprimer un statut de véhicule
        /// </summary>
        /// <param name="statusId">int: identifiant du statuts</param>
        /// <returns></returns>
        [HttpDelete("{statusId}")]
        public async Task<IActionResult> DeleteStatus(int statusId)
        {
            Response<string> response = await _StatusRepository.DeleteStatusAsync(statusId);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer tout les statuts de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStatusAsync()
        {
            
            Response<List<StatusDTO>> response = await _StatusRepository.GetAllStatusAsync();

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);

        }

        /// <summary>
        /// Permet de récupérer un de véhicule en utilisant son Id
        /// </summary>
        /// <param name="statusId">int: identifiant du statuts</param>
        /// <returns></returns>
        [HttpGet("{statusId}")]
        public async Task<IActionResult> GetStatusByIdAsync(int statusId)
        {

            Response<StatusDTO> response = await _StatusRepository.GetStatusByIdAsync(statusId);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);

        }

        /// <summary>
        /// Permet de mettre à jour un statut de véhicule
        /// </summary>
        /// <param name="statusId">int: identifiant du statuts</param>
        /// <param name="statusDTO"></param>
        /// <returns></returns>    
        [HttpPut("{statusId}")]
        public async Task<IActionResult> UpdateStatusAsync(int statusId, StatusDTO statusDTO)
        {
            Response<StatusDTO>? response = await _StatusRepository.UpdateStatusAsync(statusId, statusDTO);

            if (response.IsSuccess)
                return Ok(response);
            else if (response.CodeStatus == 404)
                return NotFound();
            else
                return Problem();
        }
    }

}
