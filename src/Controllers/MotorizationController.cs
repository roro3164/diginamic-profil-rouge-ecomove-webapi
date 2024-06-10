using Ecomove.Api.DTOs.MotorizationDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.Motorizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/motorizations")]
    //[Authorize(Roles = $"{Roles.ADMIN}")]
    public class MotorizationController(IMotorizationService motorizationService) : ControllerBase
    {
        /// <summary>
        /// Permet de créer une motorisation de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMotorization(MotorizationDTO motorizationDTO)
        {
            Response<bool> createMotorizationResult = await motorizationService.CreateMotorizationAsync(motorizationDTO);

            return new JsonResult(createMotorizationResult) { StatusCode = createMotorizationResult.CodeStatus };
        }

        /// <summary>
        /// Permet de supprimer une motorisation de véhicule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteMotorization(int id)
        {
            Response<bool> deleteMotorizationResult = await motorizationService.DeleteMotorizationAsync(id);

            return new JsonResult(deleteMotorizationResult) { StatusCode = deleteMotorizationResult.CodeStatus };

        }

        /// <summary>
        /// Permet de récupérer toutes les motorisations de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllMotorizations()
        {
            Response<List<MotorizationDTO>> getAllMotorizationsResult = await motorizationService.GetAllMotorizationsAsync();

            return new JsonResult(getAllMotorizationsResult) { StatusCode = getAllMotorizationsResult.CodeStatus };

        }

        /// <summary>
        /// Permet de récupérer une motorisation de véhicule en utilisant son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetMotorization(int id)
        {
            Response<MotorizationDTO> getMotorizationByIdResult = await motorizationService.GetMotorizationAsync(id);

            return new JsonResult(getMotorizationByIdResult) { StatusCode = getMotorizationByIdResult.CodeStatus };
        }

        /// <summary>
        /// Permet de modifier une Motorisation
        /// </summary>
        /// <param name="id">int : identifiant de la motorisation</param>
        /// <param name="motorizationDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateMotorization(int id, MotorizationDTO motorizationDTO)
        {
            Response<bool> updateMotorizationResult = await motorizationService.UpdateMotorizationAsync(id, motorizationDTO);

            return new JsonResult(updateMotorizationResult) { StatusCode = updateMotorizationResult.CodeStatus };
        }
    }
}
