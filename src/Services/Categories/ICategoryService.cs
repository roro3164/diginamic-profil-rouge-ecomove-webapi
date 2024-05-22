using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.Categories;

public interface ICategoryService
{
    Task<Response<bool>> CreateCategoryAsync(CategoryDTO category);
    Task<Response<List<CategoryDTO>>> GetAllCategoriesAsync();
    Task<Response<CategoryDTO>> GetCategoryByIdAsync(int id);
    Task<Response<bool>> UpdateCategoryAsync(int id, CategoryDTO category);
    Task<Response<bool>> DeleteCategoryAsync(int id);
}
