using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleMotorizationDTOs;
using ecomove_back.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IVehicleMotorizationRepository
    {
        public Task<Response<VehicleMotorizationForCreationDTO>> CreateVehicleMotorizationAsync(VehicleMotorizationForCreationDTO MotorizationDTO);
        Task<Response<string>> DeleteVehicleMotorizationAsync(int vehicleMotorizationId);
    }
}
