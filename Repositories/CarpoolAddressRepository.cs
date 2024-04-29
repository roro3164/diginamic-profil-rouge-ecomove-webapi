using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.AdressDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class CarpoolAddressRepository : ICarpoolAddressRepository
    {
        private readonly EcoMoveDbContext _ecoMoveDbContext;

        public CarpoolAddressRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }
        public async Task<Response<CarpoolAddressDTO>> CreateCarpoolAddressAsync(CarpoolAddressDTO carpoolAddressDTO)
        {
            try
            {
                CarpoolAddress carpoolAddress = new CarpoolAddress
                {
                    CarpoolAddressId = Guid.NewGuid(),
                    Address = carpoolAddressDTO.Address,
                    Latitude = carpoolAddressDTO.Latitude,
                    Longitude = carpoolAddressDTO.Longitude
                };

                await _ecoMoveDbContext.CarpoolAddresses.AddAsync(carpoolAddress);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAddressDTO>
                {
                    Message = $"L'addresse a bien été créée avec succès.",
                    Data = carpoolAddressDTO,
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAddressDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500,
                };
            }
        }

        public async Task<Response<CarpoolAddressDTO>> DeleteCarpoolAddressAsync(Guid id)
        {
            try
            {
                CarpoolAddress? carpoolAddress = await _ecoMoveDbContext.CarpoolAddresses.FirstOrDefaultAsync(c => c.CarpoolAddressId == id);


                // check if the carpoolAddress is not in a carpooling before deleting / updating it !!!!

                if (carpoolAddress is null)
                    return new Response<CarpoolAddressDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune adresse n'a été trouvée"

                    };

                _ecoMoveDbContext.CarpoolAddresses.Remove(carpoolAddress);

                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAddressDTO>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Message = "L'addresse est supprimée avec succès"
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAddressDTO>
                {
                    IsSuccess = false,
                    CodeStatus = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<List<CarpoolAddressDTO>>> GetAllCarpoolAddressAsync()
        {
            try
            {
                List<CarpoolAddress> carpoolAddresses = await _ecoMoveDbContext.CarpoolAddresses.ToListAsync();


                if (carpoolAddresses.Count > 0)
                {
                    List<CarpoolAddressDTO> carpoolAddressDTOs = new();

                    foreach (CarpoolAddress carpoolAddress in carpoolAddresses)
                    {
                        carpoolAddressDTOs.Add(new CarpoolAddressDTO
                        {
                            Address = carpoolAddress.Address,
                            Latitude = carpoolAddress.Latitude,
                            Longitude = carpoolAddress.Longitude,
                        });
                    }

                    return new Response<List<CarpoolAddressDTO>>
                    {
                        IsSuccess = true,
                        CodeStatus = 200,
                        Data = carpoolAddressDTOs,
                    };
                }

                return new Response<List<CarpoolAddressDTO>>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune adresse n'a été trouvée"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CarpoolAddressDTO>>
                {
                    IsSuccess = false,
                    CodeStatus = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<CarpoolAddressDTO>> GetCarpoolAddressByIdAsync(Guid id)
        {
            try
            {
                CarpoolAddress? carpoolAddress = await _ecoMoveDbContext.CarpoolAddresses
                .FirstOrDefaultAsync(c => c.CarpoolAddressId == id);

                if (carpoolAddress is null)
                    return new Response<CarpoolAddressDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune adresse n'a été trouvée"

                    };


                CarpoolAddressDTO carpoolAddressDTO = new CarpoolAddressDTO()
                {
                    Address = carpoolAddress.Address,
                    Latitude = carpoolAddress.Latitude,
                    Longitude = carpoolAddress.Longitude,
                };


                return new Response<CarpoolAddressDTO>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = carpoolAddressDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAddressDTO>
                {
                    IsSuccess = false,
                    CodeStatus = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<CarpoolAddressDTO>> UpdateCarpoolAddressAsync(Guid id, CarpoolAddressDTO carpoolAddressDTO)
        {
            try
            {
                CarpoolAddress? carpoolAddress = await _ecoMoveDbContext.CarpoolAddresses
                .FirstOrDefaultAsync(c => c.CarpoolAddressId == id);

                if (carpoolAddress is null)
                    return new Response<CarpoolAddressDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune adresse n'a été trouvée"

                    };

                carpoolAddress.Address = carpoolAddressDTO.Address;
                carpoolAddress.Latitude = carpoolAddressDTO.Latitude;
                carpoolAddress.Longitude = carpoolAddressDTO.Longitude;

                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAddressDTO>
                {
                    Message = "L'addresse a été bien mise à jour avec succés",
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAddressDTO>
                {
                    IsSuccess = false,
                    CodeStatus = 500,
                    Message = ex.Message
                };
            }
        }
    }
}