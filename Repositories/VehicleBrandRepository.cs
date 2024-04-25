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


        public async Task<Response<VehicleBrandDTO>> CreateBrandVehicleAsync(VehicleBrandDTO vehicleDTO)
        {
            Brand brandVehicle = new Brand
            {
                BrandLabel = vehicleDTO.BrandLabel
            };

            try
            {
                await _ecoMoveDbContext.Brands.AddAsync(brandVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleBrandDTO>
                {
                    Message = $"La marque { brandVehicle.BrandLabel} a bien été créé",
                    Data = vehicleDTO,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleBrandDTO>
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

        public async Task<Response<List<VehicleBrandDTO>>> GetAllBrandAysnc()
        {
            try
            {
                List<Brand> brands = await _ecoMoveDbContext.Brands.ToListAsync();

                if (brands.Count == 0)
                {
                    return new Response<List<VehicleBrandDTO>>
                    {
                        Message = "Aucune marque trouvé",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                List<VehicleBrandDTO> brandsDTO = new List<VehicleBrandDTO>();

                foreach (var brand in brands)
                {
                    brandsDTO.Add(new VehicleBrandDTO
                    {
                        BrandLabel = brand.BrandLabel
                    });
                }

                return new Response<List<VehicleBrandDTO>>
                {
                    Data = brandsDTO,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<List<VehicleBrandDTO>>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<VehicleBrandDTO>> GetBrandByIdAysnc(int brandId)
        {
            try
            {
                Brand? brand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (brand == null)
                {
                    return new Response<VehicleBrandDTO>
                    {
                        Message = "La Marque que vous voulez n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                VehicleBrandDTO brandDTO = new VehicleBrandDTO
                {
                    BrandLabel = brand.BrandLabel
                };

                return new Response<VehicleBrandDTO>
                {
                    Data = brandDTO,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleBrandDTO>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }





    }
}