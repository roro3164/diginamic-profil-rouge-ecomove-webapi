using Ecomove.Api.Data;
using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    // [Authorize(Roles = $"{Roles.ADMIN}")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        /// <summary>
        /// Permet de créer une catégorie de véhicules
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDto)
        {
            Response<bool> createCategoryResult = await categoryService.CreateCategoryAsync(categoryDto);

            return new JsonResult(createCategoryResult) { StatusCode = createCategoryResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer toutes les catégories de véhicules
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCategories()
        {
            Response<List<CategoryDTO>> getAllCategoriesResult = await categoryService.GetAllCategoriesAsync();

            return new JsonResult(getAllCategoriesResult) { StatusCode = getAllCategoriesResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer une catégorie de véhicules par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Response<CategoryDTO> getCategoryByIdResult = await categoryService.GetCategoryByIdAsync(id);

            return new JsonResult(getCategoryByIdResult) { StatusCode = getCategoryByIdResult.CodeStatus };
        }


        /// <summary>
        /// Permet de mettre à jour une catégorie de véhicules par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            Response<bool> updateCategoryResult = await categoryService.UpdateCategoryAsync(id, categoryDto);

            return new JsonResult(updateCategoryResult) { StatusCode = updateCategoryResult.CodeStatus };
        }


        /// <summary>
        /// Permet de supprimer une catégorie de véhicules par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Response<bool> deleteCategoryResult = await categoryService.DeleteCategoryAsync(id);

            return new JsonResult(deleteCategoryResult) { StatusCode = deleteCategoryResult.CodeStatus };
        }
    }
}