using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleCategoryDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class VehicleCategoryRepository : IVehicleCategoryRepository
    {
        private readonly EcoMoveDbContext _ecoMoveDbContext;

        public VehicleCategoryRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<VehicleCategoryForCreationDTO>> CreateVehicleCategoryAsync(VehicleCategoryForCreationDTO categoryDTO)
        {
            Category category = new Category
            {
                CategoryLabel = categoryDTO.CategroyLabel,
            };

            try
            {
                await _ecoMoveDbContext.Categories.AddAsync(category);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleCategoryForCreationDTO>
                {
                    Message = $"La catégorie {category.CategoryLabel} a bien été créée",
                    Data = categoryDTO,
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleCategoryForCreationDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                };
            }
        }

        public async Task<Response<string>> DeleteVehicleCategoryAsync(int cateogoryId)
        {
            Category? category = await _ecoMoveDbContext
            .Categories.FirstOrDefaultAsync(category => category.CategoryId == cateogoryId);

            if (category is null)
            {
                return new Response<string>
                {
                    Message = "La catégorie que vous voulez supprimer n'existe pas.",
                    IsSuccess = false,
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

        public Task<Response<List<Category>>> GetAllVehiclesCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> GetVehicleCategoryByIdAsync(int cateogoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> UpdateVehicleCategoryAsync(int categoryId, VehicleCategoryForCreationDTO category)
        {
            throw new NotImplementedException();
        }
    }
}