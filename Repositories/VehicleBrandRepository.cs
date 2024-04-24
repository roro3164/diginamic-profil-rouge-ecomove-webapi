using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleBrandDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;

namespace ecomove_back.Repositories
{
    public class VehicleBrandRepository : IVehicleBrandRepository
    {
        private EcoMoveDbContext _dbContext;

        public VehicleBrandRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _dbContext = ecoMoveDbContext;
        }


        public async Task<Response<VehicleBrandForCreationDTO>> CreateBrandVehicleAsync(VehicleBrandForCreationDTO vehicleDTO)
        {
            Brand brandVehicle = new Brand
            {
                BrandLabel = vehicleDTO.BrandLabel
            };

            try
            {
                await _dbContext.Brands.AddAsync(brandVehicle);
                await _dbContext.SaveChangesAsync();

                return new Response<VehicleBrandForCreationDTO>
                {
                    Message = $"La marque \"{ brandVehicle.BrandLabel}\" a bien été créé",
                    Data = vehicleDTO,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleBrandForCreationDTO>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }


    }
}