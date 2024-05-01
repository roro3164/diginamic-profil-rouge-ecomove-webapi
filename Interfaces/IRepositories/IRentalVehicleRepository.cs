using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.DTOs.RentalVehicleDTO;
using ecomove_back.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IRentalVehicleRepository
    {
        public Task<Response<string>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO);
        public Task<Response<RentalVehicleDTO>> UpdateRentalVehicleAsync(Guid rentalId, RentalVehicleDTO userDTO);


    }
}