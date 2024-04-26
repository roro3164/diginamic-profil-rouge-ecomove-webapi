using ecomove_back.DTOs.CategoryDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ecomove_back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        /// <summary>
        /// Permet de créer une catégorie de véhicules
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO vehicleCategory)
        {
            Response<CategoryDTO> response = await _categoryRepository.CreateCategoryAsync(vehicleCategory);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer une catégories de véhicule en utilisant son Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Response<CategoryDTO> response = await _categoryRepository.GetCategoryByIdAsync(id);

            if (response.IsSuccess)
                return Ok(response.Data);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }

        /// <summary>
        /// Permet de récupérer toutes les catégories de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            Response<List<CategoryDTO>> response = await _categoryRepository.GetAllCategoriesAsync();

            if (response.IsSuccess)
                return Ok(response.Data);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        /// <summary>
        /// Permet de supprimer une catégorie de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Response<string> response = await _categoryRepository.DeleteCategoryAsync(id);

            if (response.IsSuccess)
                return Ok(response.Message);
            else if (response.CodeStatus == 404)
                return NotFound(response.Message);
            else
                return Problem(response.Message);
        }


        /// <summary>
        /// Permet de mettre à jour une catégorie de véhicule
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(int id, CategoryDTO categoryDTO)
        {
            Response<CategoryDTO>? respone = await _categoryRepository.UpdateCategoryAsync(id, categoryDTO);

            if (respone.IsSuccess)
                return Ok(respone);
            else if (respone.CodeStatus == 404)
                return NotFound();
            else
                return Problem();
        }
    }
}
