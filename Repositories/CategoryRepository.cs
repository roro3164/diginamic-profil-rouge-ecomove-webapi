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

        public async Task<Response<VehicleCategoryDTO>> CreateVehicleCategoryAsync(VehicleCategoryDTO VehicleCategoryDTO)
        {
            Category category = new Category
            {
                CategoryLabel = VehicleCategoryDTO.CategoryLabel,
            };

            try
            {
                await _ecoMoveDbContext.Categories.AddAsync(category);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleCategoryDTO>
                {
                    Message = $"La catégorie {category.CategoryLabel} a bien été créée",
                    Data = VehicleCategoryDTO,
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleCategoryDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                };
            }
        }

        public async Task<Response<string>> DeleteVehicleCategoryAsync(int cateogoryId)
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

            try
            {
                _ecoMoveDbContext.Categories.Remove(category);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"La catégorie {category.CategoryLabel} a été supprimée avec succés.",
                    IsSuccess = true
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

        public async Task<Response<List<VehicleCategoryDTO>>> GetAllVehiclesCategoriesAsync()
        {
            try
            {
                List<Category> categories = await _ecoMoveDbContext.Categories.ToListAsync();

                List<VehicleCategoryDTO> vehicleCategoriesDTO = new();

                foreach (var category in categories)
                {
                    vehicleCategoriesDTO.Add(new VehicleCategoryDTO { CategoryLabel = category.CategoryLabel });
                }

                if (categories.Count > 0)
                {
                    return new Response<List<VehicleCategoryDTO>>
                    {
                        IsSuccess = true,
                        Data = vehicleCategoriesDTO,
                        Message = null,
                        CodeStatus = 200,
                    };
                }
                else
                {
                    return new Response<List<VehicleCategoryDTO>>
                    {
                        IsSuccess = false,
                        Message = "La liste des catégories est vide",
                        CodeStatus = 404
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response<List<VehicleCategoryDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<Response<VehicleCategoryDTO>> GetVehicleCategoryByIdAsync(int cateogoryId)
        {
            try
            {
                Category? category = await _ecoMoveDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == cateogoryId);


                if (category is null)
                {
                    return new Response<VehicleCategoryDTO>
                    {
                        CodeStatus = 404,
                        Message = $"La catégorie que vous recherchez n'existe pas.",
                        IsSuccess = false
                    };
                }
                else
                {
                    VehicleCategoryDTO vehicleVehicleCategoryDTO = new() { CategoryLabel = category.CategoryLabel };

                    return new Response<VehicleCategoryDTO>
                    {
                        Data = vehicleVehicleCategoryDTO,
                        CodeStatus = 200,
                        IsSuccess = true,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<VehicleCategoryDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }

        public async Task<Response<VehicleCategoryDTO>> UpdateVehicleCategoryAsync(int categoryId, VehicleCategoryDTO VehicleCategoryDTO)
        {
            try
            {
                Category? category = await _ecoMoveDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

                if (category is null)
                {
                    return new Response<VehicleCategoryDTO>
                    {
                        CodeStatus = 404,
                        Message = "La catégorie que vous recherchez n'existe pas",
                        IsSuccess = false,
                    };
                }

                category.CategoryLabel = VehicleCategoryDTO.CategoryLabel;
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleCategoryDTO>
                {
                    Message = $"La catégorie a été bien modifiéée",
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<VehicleCategoryDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500,
                };
            }
        }
    }
}