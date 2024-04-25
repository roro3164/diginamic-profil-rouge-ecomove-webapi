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
        public Task<Response<VehicleStatusDTO>> CreateVehicleStatusAsync(VehicleStatusDTO StatusDTO);
        public Task<Response<string>> DeleteVehicleStatusAsync(int StatusId);
        public Task<Response<List<VehicleStatusDTO>>> GetAllVehicleStatusAsync();
        public Task<Response<VehicleStatusDTO>> GetByIdVehicleStatusAsync(int id);


    }
}