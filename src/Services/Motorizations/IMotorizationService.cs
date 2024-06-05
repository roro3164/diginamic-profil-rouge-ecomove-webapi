using Ecomove.Api.DTOs.MotorizationDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.Motorizations;

public interface IMotorizationService
{
    Task<Response<bool>> CreateMotorizationAsync(MotorizationDTO motorization);
    Task<Response<List<MotorizationDTO>>> GetAllMotorizationsAsync();
    Task<Response<MotorizationDTO>> GetMotorizationAsync(int id);
    Task<Response<bool>> UpdateMotorizationAsync(int id, MotorizationDTO motorization);
    Task<Response<bool>> DeleteMotorizationAsync(int id);
}