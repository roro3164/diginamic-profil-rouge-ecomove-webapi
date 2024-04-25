using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleStatusDTOs;
using ecomove_back.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IVehicleStatusRepository
    {
        public Task<Response<VehicleStatusForCreationDTO>> CreateVehicleStatusAsync(VehicleStatusForCreationDTO StatusDTO);
        public Task<Response<string>> DeleteVehicleStatusAsync(int StatusId);
    }
}