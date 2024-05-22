using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CategoryDTOs;
using ErrorOr;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<ErrorOr<Created>> CreateCategoryAsync(CategoryDTO categoryDto);
        Task<ErrorOr<List<Category>>> GetAllCategoriesAsync();
        Task<ErrorOr<Category>> GetCategoryByIdAsync(int id);
        Task<ErrorOr<Updated>> UpdateCategoryAsync(int id, CategoryDTO categoryDto);
        Task<ErrorOr<Deleted>> DeleteCategoryAsync(int id);
    }
}