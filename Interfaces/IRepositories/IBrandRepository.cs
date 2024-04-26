using ecomove_back.DTOs.BrandDTOs;
using ecomove_back.Helpers;


namespace ecomove_back.Interfaces.IRepositories
{
    public interface IBrandRepository
    {
        public Task<Response<BrandDTO>> CreateBrandAsync(BrandDTO vehicleBrandForCreationDTO);
        public Task<Response<string>> DeleteBrandAsync(int brandId);
        public Task<Response<List<BrandDTO>>> GetAllBrandAysnc();
        public Task<Response<BrandDTO>> GetBrandByIdAysnc(int brandId);
        public Task<Response<BrandDTO>> UpdateBrandAysnc(int brandId, BrandDTO brandDTO);

    }
}