using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger) : ICategoryService
{
    // Create a new category
    public async Task<Response<bool>> CreateCategoryAsync(CategoryDTO categoryDto)
    {
        ErrorOr<Created> createCategoryResult = await categoryRepository.CreateCategoryAsync(categoryDto);

        return createCategoryResult.MatchFirst(created =>
        {
            logger.LogInformation($"Category {categoryDto.CategoryLabel} created successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                Message = "La catégorie a bien été créée avec succès",
                CodeStatus = 201,
                Data = true
            };
        }, error =>
        {
            logger.LogError(createCategoryResult.FirstError.Description);

            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la création de la catégorie",
                CodeStatus = 500,
                Data = false,
            };
        });
    }
    // Get all categories from the database
    public async Task<Response<List<CategoryDTO>>> GetAllCategoriesAsync()
    {
        ErrorOr<List<Category>> getCategoriesResult = await categoryRepository.GetAllCategoriesAsync();

        return getCategoriesResult.MatchFirst(categories =>
        {
            logger.LogInformation("Categories fetched successfully !");

            return new Response<List<CategoryDTO>>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = getCategoriesResult.Value.Adapt<List<CategoryDTO>>()
            };

        }, error =>
        {
            logger.LogError(getCategoriesResult.FirstError.Description);

            return new Response<List<CategoryDTO>>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la récupération des catégories",
                CodeStatus = 500
            };
        });
    }
    // Get a category by ID
    public async Task<Response<CategoryDTO>> GetCategoryByIdAsync(int id)
    {
        ErrorOr<Category> getCategroyResult = await categoryRepository.GetCategoryByIdAsync(id);

        return getCategroyResult.MatchFirst(category =>
        {
            logger.LogInformation($"Category with ID {id} fetched successfully !");

            return new Response<CategoryDTO>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = getCategroyResult.Value.Adapt<CategoryDTO>()
            };

        }, error =>
        {
            logger.LogError(getCategroyResult.FirstError.Description);

            if (getCategroyResult.FirstError.NumericType == (int)ErrorType.NotFound)
            {
                return new Response<CategoryDTO>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune catégorie n'a été trouvée"
                };
            }

            return new Response<CategoryDTO>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la récupération de la catégorie",
                CodeStatus = 500
            };
        });

    }
    // Update a category
    public async Task<Response<bool>> UpdateCategoryAsync(int id, CategoryDTO categoryDto)
    {
        ErrorOr<Updated> updateCategoryResult = await categoryRepository.UpdateCategoryAsync(id, categoryDto);

        return updateCategoryResult.MatchFirst(category =>
        {
            logger.LogInformation($"Category with ID {id} had been updated successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                CodeStatus = 201,
                Message = "La catégorie a bien été mise à jour"
            };

        }, error =>
        {
            logger.LogError(updateCategoryResult.FirstError.Description);

            if (updateCategoryResult.FirstError.NumericType == (int)ErrorType.NotFound)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune catégorie n'a été trouvée",
                    Data = false,
                };
            }


            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la mise à jour de la catégorie",
                CodeStatus = 500,
                Data = false,
            };
        });
    }
    // Delete a category
    public async Task<Response<bool>> DeleteCategoryAsync(int id)
    {
        ErrorOr<Deleted> deleteCategoryResult = await categoryRepository.DeleteCategoryAsync(id);

        return deleteCategoryResult.MatchFirst(category =>
        {
            logger.LogInformation($"Category with ID {id} had been deleted successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Message = "La catégorie a bien été supprimée"
            };

        }, error =>
        {
            logger.LogError(deleteCategoryResult.FirstError.Description);

            if (deleteCategoryResult.FirstError.NumericType == (int)ErrorType.NotFound)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune catégorie n'a été trouvée",
                    Data = false,
                };
            }

            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la suppression de la catégorie",
                CodeStatus = 500,
                Data = false
            };
        });
    }
}