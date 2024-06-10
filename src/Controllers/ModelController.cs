using Ecomove.Api.DTOs.ModelDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/model")]
    //[Authorize(Roles = $"{Roles.ADMIN}")]
    public class ModelController(IModelService modelService) : ControllerBase
    {
        /// <summary>
        /// Permet de créer un modèle de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateModelAsync(ModelFKeyDTO modelFKeyDTO)
        {
            Response<bool> createModelResult = await modelService.CreateModelAsync(modelFKeyDTO);

            return new JsonResult(createModelResult) { StatusCode = createModelResult.CodeStatus };
        }

        /// <summary>
        /// Permet de supprimer un modèle de véhicule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteModelAsync(int id)
        {
            Response<bool> deleteModelResult = await modelService.DeleteModelAsync(id);

            return new JsonResult(deleteModelResult) { StatusCode = deleteModelResult.CodeStatus };
        }

        /// <summary>
        /// Permet de récupérer toutes les modèles de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            Response<List<ModelDTO>> getAllModelsResult = await modelService.GetAllModelsAsync();

            return new JsonResult(getAllModelsResult) { StatusCode = getAllModelsResult.CodeStatus };

        }

        /// <summary>
        /// Permet de récupérer un modèle de véhicule en utilisant son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetModelById(int id)
        {

            Response<ModelDTO> getModelByIdResult = await modelService.GetModelByIdAsync(id);

            return new JsonResult(getModelByIdResult) { StatusCode = getModelByIdResult.CodeStatus };
        }

        /// <summary>
        /// Permet de modifier un modèle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modelDTO >"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateModelById(int id, ModelDTO modelDTO)
        {
            Response<bool> updateCategoryResult = await modelService.UpdateModelByIdAsync(id, modelDTO);

            return new JsonResult(updateCategoryResult) { StatusCode = updateCategoryResult.CodeStatus };
        }
    }
}
