using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.Brands
{
    public interface IBrandService
    {
        Task<Response<bool>> CreateBrandAsync(BrandDTO brandDTO);
        Task<Response<List<BrandDTO>>> GetAllBrandsAsync();
        Task<Response<BrandDTO>> GetBrandByIdAsync(int id);
        Task<Response<bool>> UpdateBrandAsync(int id, BrandDTO brandDTO);
        Task<Response<bool>> DeleteBrandAsync(int id);
    }
}
