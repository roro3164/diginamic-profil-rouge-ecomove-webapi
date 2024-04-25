using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleVehicleCategoryDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcoMoveDbContext _ecoMoveDbContext;

        public CategoryRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<CategoryDTO>> CreateCategoryAsync(CategoryDTO categoryDTO)
        {
            try
            {
                Category category = new Category
                {
                    CategoryLabel = categoryDTO.CategoryLabel,
                };

                await _ecoMoveDbContext.Categories.AddAsync(category);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CategoryDTO>
                {
                    Message = $"La catégorie {category.CategoryLabel} a bien été créée",
                    Data = categoryDTO,
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500,
                };
            }
        }

        public async Task<Response<string>> DeleteCategoryAsync(int cateogoryId)
        {
            try
            {
                Category? category = await _ecoMoveDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == cateogoryId);

                if (category is null)
                {
                    return new Response<string>
                    {
                        Message = "La catégorie que vous voulez supprimer n'existe pas.",
                        IsSuccess = false,
                        CodeStatus = 404,
                    };
                }
                _ecoMoveDbContext.Categories.Remove(category);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"La catégorie {category.CategoryLabel} a été supprimée avec succés.",
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<List<CategoryDTO>>> GetAllCategoriesAsync()
        {
            try
            {
                List<Category> categories = await _ecoMoveDbContext.Categories.ToListAsync();

                List<CategoryDTO> vehicleCategoriesDTO = new();

                foreach (var category in categories)
                {
                    vehicleCategoriesDTO.Add(new CategoryDTO { CategoryLabel = category.CategoryLabel });
                }

                if (categories.Count > 0)
                {
                    return new Response<List<CategoryDTO>>
                    {
                        IsSuccess = true,
                        Data = vehicleCategoriesDTO,
                        Message = null,
                        CodeStatus = 200,
                    };
                }
                else
                {
                    return new Response<List<CategoryDTO>>
                    {
                        IsSuccess = false,
                        Message = "La liste des catégories est vide",
                        CodeStatus = 404
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response<List<CategoryDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<Response<CategoryDTO>> GetCategoryByIdAsync(int cateogoryId)
        {
            try
            {
                Category? category = await _ecoMoveDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == cateogoryId);

                if (category is null)
                {
                    return new Response<CategoryDTO>
                    {
                        Message = $"La catégorie que vous recherchez n'existe pas.",
                        IsSuccess = false,
                        CodeStatus = 404,
                    };
                }
                else
                {
                    CategoryDTO vehicleCategoryDTO = new() { CategoryLabel = category.CategoryLabel };

                    return new Response<CategoryDTO>
                    {
                        Data = vehicleCategoryDTO,
                        CodeStatus = 200,
                        IsSuccess = true,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }

        public async Task<Response<CategoryDTO>> UpdateCategoryAsync(int categoryId, CategoryDTO categoryDTO)
        {
            try
            {
                Category? category = await _ecoMoveDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

                if (category is null)
                {
                    return new Response<CategoryDTO>
                    {
                        Message = "La catégorie que vous recherez n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404,
                    };
                }

                category.CategoryLabel = categoryDTO.CategoryLabel;
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CategoryDTO>
                {
                    Message = $"La catégorie a été bien modifiéée",
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>
                {
                    Message = ex.Message,
                    IsSuccess = true,
                    CodeStatus = 500,
                };
            }
        }
    }
}