using Ecomove.Api.DTOs.MotorizationDTOs;
using Ecomove.Api.Helpers;


namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IMotorizationRepository
    {
        Task<Response<MotorizationDTO>> CreateMotorizationAsync(MotorizationDTO motorizationDTO);
        Task<Response<string>> DeleteMotorizationAsync(int motorizationId);
        Task<Response<List<MotorizationDTO>>> GetAllMotorizationsAsync();
        Task<Response<MotorizationDTO>> GetMotorizationByIdAsync(int motorizationId);
        Task<Response<MotorizationDTO>> UpdateMotorizationByIdAsync(int motorizationId, MotorizationDTO MotorizationDTO);
    }
}
