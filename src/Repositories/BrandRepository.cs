using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class BrandRepository(EcoMoveDbContext ecoMoveDbContext) : IBrandRepository
    {
        public async Task<ErrorOr<Created>> CreateBrandAsync(BrandDTO brandDTO)
        {
            var existingBrand = await ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandLabel == brandDTO.BrandLabel);

            if (existingBrand != null) return Error.Conflict(description: $"La marque {brandDTO.BrandLabel} existe déjà en base de données.");

            try
            {
                Brand brand = new Brand { BrandLabel = brandDTO.BrandLabel };

                await ecoMoveDbContext.Brands.AddAsync(brand);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Created;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        public async Task<ErrorOr<Deleted>> DeleteBrandAsync(int brandId)
        {
            try
            {
                Brand? brand = await ecoMoveDbContext.Brands
                    .Include(b => b.Models)
                    .FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (brand is null) return Error.NotFound(description: $"La Marque que vous voulez supprimer n'existe pas");
                if (brand?.Models?.Count != 0) return Error.Conflict(description: $"Vous ne pouvez pas supprimer cette marque car des modèles y sont associés");

                ecoMoveDbContext.Brands.Remove(brand);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        public async Task<ErrorOr<List<Brand>>> GetAllBrandsAsync()
        {
            try
            {
                return await ecoMoveDbContext.Brands.ToListAsync();
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        public async Task<ErrorOr<Brand>> GetBrandByIdAsync(int brandId)
        {
            try
            {
                Brand? brand = await ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (brand is null) return Error.NotFound(description: $"La Marque que vous voulez n'existe pas.");

                return brand;
            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }
        }


        public async Task<ErrorOr<Updated>> UpdateBrandAsync(int brandId, BrandDTO brandDTO)
        {
            try
            {
                Brand? brand = await ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (brand is null) return Error.NotFound(description: $"La Marque que vous voulez modifier n'existe pas");

                brand.BrandLabel = brandDTO.BrandLabel;

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Updated;

            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
    }
}
