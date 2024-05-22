using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IBrandRepository
    {
        public Task<Response<BrandDTO>> CreateBrandAsync(BrandDTO brandDTO);
        public Task<Response<string>> DeleteBrandAsync(int brandId);
        public Task<Response<List<BrandDTO>>> GetAllBrandAysnc(string search);
        public Task<Response<BrandDTO>> GetBrandByIdAysnc(int brandId);
        public Task<Response<BrandDTO>> UpdateBrandAysnc(int brandId, BrandDTO brandDTO);
    }
}