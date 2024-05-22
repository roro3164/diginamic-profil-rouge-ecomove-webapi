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
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDto)
        {
            Response<bool> createCategoryResult = await categoryService.CreateCategoryAsync(categoryDto);

            return new JsonResult(createCategoryResult) { StatusCode = createCategoryResult.CodeStatus };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            Response<List<CategoryDTO>> getAllCategoriesResult = await categoryService.GetAllCategoriesAsync();

            return new JsonResult(getAllCategoriesResult) { StatusCode = getAllCategoriesResult.CodeStatus };
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Response<CategoryDTO> getCategoryByIdResult = await categoryService.GetCategoryByIdAsync(id);

            return new JsonResult(getCategoryByIdResult) { StatusCode = getCategoryByIdResult.CodeStatus };
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            var updateCategoryResult = await categoryService.UpdateCategoryAsync(id, categoryDto);

            return new JsonResult(updateCategoryResult) { StatusCode = updateCategoryResult.CodeStatus };
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Response<bool> deleteCategoryResult = await categoryService.DeleteCategoryAsync(id);

            return new JsonResult(deleteCategoryResult) { StatusCode = deleteCategoryResult.CodeStatus };
        }
    }
}