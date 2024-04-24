using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleCategoryDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IVehicleCategoryRepository
    {
        Task<Response<Category>> CreateVehicleCategoryAsync(VehicleCategoryForCreationDTO category);
        Task<Response<Category>> UpdateVehicleCategoryAsync(int categoryId, VehicleCategoryForCreationDTO category);
        Task<Response<string>> DeleteVehicleCategoryAsync(int cateogoryId);
        Task<Response<Category>> GetVehicleCategoryByIdAsync(int cateogoryId);
        Task<Response<List<Category>>> GetAllVehiclesCategoryAsync();
    }
}