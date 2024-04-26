using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleMotorizationDTOs;
using ecomove_back.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IMotorizationRepository
    {
        public Task<Response<VehicleMotorizationDTO>> CreateVehicleMotorizationAsync(VehicleMotorizationDTO MotorizationDTO);
        Task<Response<string>> DeleteVehicleMotorizationAsync(int vehicleMotorizationId);
        Task<Response<List<Motorization>>> GetAllVehicleMotorizationsAsync();
        Task<Response<int>> GetVehicleMotorizationByIdAsync(int motorizationId);
    }
}
