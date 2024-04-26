using ecomove_back.Data.Models;
using ecomove_back.DTOs.MotorizationDTOs;
using ecomove_back.Helpers;


namespace ecomove_back.Interfaces.IRepositories
{
    public interface IMotorizationRepository
    {
        public Task<Response<MotorizationDTO>> CreateMotorizationAsync(MotorizationDTO motorizationDTO);
        Task<Response<string>> DeleteMotorizationAsync(int motorizationId);
        Task<Response<List<Motorization>>> GetAllMotorizationsAsync();
        Task<Response<int>> GetMotorizationByIdAsync(int motorizationId);
        Task<Response<MotorizationDTO>> UpdateMotorizationByIdAsync(int motorizationId, MotorizationDTO MotorizationDTO);
    }
}
