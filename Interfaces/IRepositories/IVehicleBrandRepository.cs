using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleBrandDTOs;
using ecomove_back.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IVehicleBrandRepository
    {
        public Task<Response<VehicleBrandForCreationDTO>> CreateBrandVehicleAsync(VehicleBrandForCreationDTO vehicleBrandForCreationDTO);
        public Task<Response<string>> DeleteBrandVehicleAsync(int brandId);

    }
}