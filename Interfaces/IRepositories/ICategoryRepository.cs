using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleVehicleCategoryDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Response<VehicleCategoryDTO>> CreateVehicleCategoryAsync(VehicleCategoryDTO category);
        Task<Response<VehicleCategoryDTO>> UpdateVehicleCategoryAsync(int categoryId, VehicleCategoryDTO category);
        Task<Response<string>> DeleteVehicleCategoryAsync(int cateogoryId);
        Task<Response<VehicleCategoryDTO>> GetVehicleCategoryByIdAsync(int cateogoryId);
        Task<Response<List<VehicleCategoryDTO>>> GetAllVehiclesCategoriesAsync();
    }
}