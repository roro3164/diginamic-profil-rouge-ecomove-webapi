using ecomove_back.DTOs.VehicleVehicleCategoryDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Response<CategoryDTO>> CreateCategoryAsync(CategoryDTO category);
        Task<Response<CategoryDTO>> UpdateCategoryAsync(int categoryId, CategoryDTO category);
        Task<Response<string>> DeleteCategoryAsync(int cateogoryId);
        Task<Response<CategoryDTO>> GetCategoryByIdAsync(int cateogoryId);
        Task<Response<List<CategoryDTO>>> GetAllCategoriesAsync();
    }
}