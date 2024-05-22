using Ecomove.Api.DTOs.AdressDTOs;
using Ecomove.Api.DTOs.CarpoolAddressDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface ICarpoolAddressRepository
    {
        Task<Response<CarpoolAddressOutGoingDTO>> CreateCarpoolAddressAsync(CarpoolAddressDTO carpoolAddressDTO);
        Task<Response<List<CarpoolAddressOutGoingDTO>>> GetAllCarpoolAddressesAsync();
        Task<Response<CarpoolAddressDTO>> GetCarpoolAddressByIdAsync(Guid id);
        Task<Response<CarpoolAddressDTO>> UpdateCarpoolAddressAsync(Guid id, CarpoolAddressDTO carpoolAddressDTO);
        Task<Response<CarpoolAddressDTO>> DeleteCarpoolAddressAsync(Guid id);

    }
}