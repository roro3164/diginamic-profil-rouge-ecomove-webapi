
using ecomove_back.DTOs.AdressDTOs;
using ecomove_back.DTOs.CarpoolAddressDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
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