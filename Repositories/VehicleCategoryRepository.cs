using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleCategoryDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;

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
                CategroyLabel = categoryDTO.CategroyLabel,
            };

            try
            {
                await _ecoMoveDbContext.Categories.AddAsync(category);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleCategoryForCreationDTO>
                {
                    Message = $"La catégorie {category.CategroyLabel} a été bien créée",
                    Data = categoryDTO,
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleCategoryForCreationDTO>
                {
                    Message = e.Message,
                    Data = null,
                    IsSuccess = false,
                };
            }
        }

        public Task<Response<string>> DeleteVehicleCategoryAsync(int cateogoryId)
        {
            throw new NotImplementedException();
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