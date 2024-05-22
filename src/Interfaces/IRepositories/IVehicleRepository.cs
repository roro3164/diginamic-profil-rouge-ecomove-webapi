using Ecomove.Api.DTOs.VehicleDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IVehicleRepository
    {
        public Task<Response<VehicleForCreateDTO>> CreateVehicleAsync(VehicleForCreateDTO VehicleForCreationDTO);
        public Task<Response<List<VehicleForGetDTO>>> GetAllVehiclesAsync();
        public Task<Response<VehicleForGetDTO>> GetVehicleByIdAsync(Guid vehicleId);
        public Task<Response<VehicleForGetByIdForAdminDTO>> GetVehicleByIdForAdminAsync(Guid vehicleId);
        public Task<Response<VehicleForUpdateDTO>> UpdateVehicleAsync(Guid vehicleId, VehicleForUpdateDTO vehicleDto);
        public Task<Response<VehicleForChangeStatusDTO>> ChangeVehicleStatusAsync(Guid vehicleId, VehicleForChangeStatusDTO statusDto);
        public Task<Response<bool>> DeleteVehicleAsync(Guid vehicleId);
    }
}