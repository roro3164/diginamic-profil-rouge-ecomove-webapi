using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleBrandDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class VehicleBrandRepository : IVehicleBrandRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;

        public VehicleBrandRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }


        public async Task<Response<VehicleBrandForCreationDTO>> CreateBrandVehicleAsync(VehicleBrandForCreationDTO vehicleDTO)
        {
            Brand brandVehicle = new Brand
            {
                BrandLabel = vehicleDTO.BrandLabel
            };

            try
            {
                await _ecoMoveDbContext.Brands.AddAsync(brandVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleBrandForCreationDTO>
                {
                    Message = $"La marque { brandVehicle.BrandLabel} a bien été créé",
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

        public async Task<Response<string>> DeleteBrandVehicleAsync(int brandId)
        {
            Brand? brand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b  => b.BrandId == brandId);

            if (brand == null)
            {
                return new Response<string>
                {
                    Message = "La Marque que vous voulez supprimer n'existe pas",
                    IsSuccess = false, 
                    CodeStatus = 404    
                };
            }

            try
            {
                _ecoMoveDbContext.Brands.Remove(brand);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"La marque {brand.BrandLabel} a bien été suprimée",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<string>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }





        }
    }
}