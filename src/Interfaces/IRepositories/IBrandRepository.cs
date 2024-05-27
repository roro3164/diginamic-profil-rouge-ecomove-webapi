using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.BrandDTOs;
using ErrorOr;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IBrandRepository
    {
        Task<ErrorOr<Created>> CreateBrandAsync(BrandDTO brandDTO);
        Task<ErrorOr<List<Brand>>> GetAllBrandsAsync();
        Task<ErrorOr<Brand>> GetBrandByIdAsync(int id);
        Task<ErrorOr<Updated>> UpdateBrandAsync(int id, BrandDTO brandDTO);
        Task<ErrorOr<Deleted>> DeleteBrandAsync(int id);
    }
}