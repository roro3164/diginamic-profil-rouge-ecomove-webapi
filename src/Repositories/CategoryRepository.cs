using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class CategoryRepository(EcoMoveDbContext ecoMoveDbContext) : ICategoryRepository
    {
        public async Task<ErrorOr<Created>> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            try
            {
                Category category = new Category { CategoryLabel = categoryDto.CategoryLabel };

                await ecoMoveDbContext.Categories.AddAsync(category);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Created;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
        public async Task<ErrorOr<List<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                return await ecoMoveDbContext.Categories.ToListAsync();
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
        public async Task<ErrorOr<Category>> GetCategoryByIdAsync(int id)
        {
            try
            {
                Category? category = await ecoMoveDbContext.Categories.FindAsync(id);

                if (category is null) return Error.NotFound(description: $"Category with ID {id} not found.");

                return category;
            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }
        }
        public async Task<ErrorOr<Updated>> UpdateCategoryAsync(int id, CategoryDTO categoryDto)
        {
            try
            {
                Category? category = await ecoMoveDbContext.Categories.FindAsync(id);

                if (category is null) return Error.NotFound(description: $"Category with ID {id} not found.");

                category.CategoryLabel = categoryDto.CategoryLabel;

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Updated;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
        public async Task<ErrorOr<Deleted>> DeleteCategoryAsync(int id)
        {
            try
            {
                Category? category = await ecoMoveDbContext.Categories.FindAsync(id);

                if (category is null) return Error.NotFound(description: $"Category with ID {id} not found.");

                ecoMoveDbContext.Categories.Remove(category);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

    }
}