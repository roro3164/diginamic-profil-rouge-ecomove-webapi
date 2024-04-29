using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.BrandDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;

        public BrandRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<BrandDTO>> CreateBrandAsync(BrandDTO brandDTO)
        {
            var existingBrand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandLabel == brandDTO.BrandLabel);

            if (existingBrand != null)
            {
                return new Response<BrandDTO>
                {
                    Message = $"La marque {brandDTO.BrandLabel} existe déjà en base de données.",
                    IsSuccess = false,
                    CodeStatus = 400
                };
            }

            Brand brand = new Brand
            {
                BrandLabel = brandDTO.BrandLabel
            };

            try
            {
                await _ecoMoveDbContext.Brands.AddAsync(brand);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<BrandDTO>
                {
                    Message = $"La marque {brand.BrandLabel} a bien été créée",
                    Data = brandDTO,
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<BrandDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<string>> DeleteBrandAsync(int brandId)
        {
            Brand? brand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);

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
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<string>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<List<BrandDTO>>> GetAllBrandAysnc()
        {
            try
            {
                List<Brand> brands = await _ecoMoveDbContext.Brands.ToListAsync();

                if (brands.Count == 0)
                {
                    return new Response<List<BrandDTO>>
                    {
                        Message = "Aucune marque trouvé",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                List<BrandDTO> brandsDTO = new List<BrandDTO>();

                foreach (var brand in brands)
                {
                    brandsDTO.Add(new BrandDTO
                    {
                        BrandLabel = brand.BrandLabel
                    });
                }

                return new Response<List<BrandDTO>>
                {
                    Data = brandsDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<List<BrandDTO>>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<BrandDTO>> GetBrandByIdAysnc(int brandId)
        {
            try
            {
                Brand? brand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (brand == null)
                {
                    return new Response<BrandDTO>
                    {
                        Message = "La Marque que vous voulez n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                BrandDTO brandDTO = new BrandDTO
                {
                    BrandLabel = brand.BrandLabel
                };

                return new Response<BrandDTO>
                {
                    Data = brandDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<BrandDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<BrandDTO>> UpdateBrandAysnc(int brandId, BrandDTO brandDTO)
        {
            try
            {
                Brand? brand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (brand == null)
                {
                    return new Response<BrandDTO>
                    {
                        Message = "La Marque que vous voulez modifier n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                brand.BrandLabel = brandDTO.BrandLabel;

                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<BrandDTO>
                {
                    Message = "La Marque a bien été modifiée",
                    Data = brandDTO,
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<BrandDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }

        }
    }
}
