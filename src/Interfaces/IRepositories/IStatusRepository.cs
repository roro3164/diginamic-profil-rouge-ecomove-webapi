using Ecomove.Api.DTOs.StatusDTOs;
using Ecomove.Api.Helpers;


namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IStatusRepository
    {
        public Task<Response<StatusDTO>> CreateStatusAsync(StatusDTO StatusDTO);
        public Task<Response<string>> DeleteStatusAsync(int StatusId);
        public Task<Response<List<StatusDTO>>> GetAllStatusAsync();
        public Task<Response<StatusDTO>> GetStatusByIdAsync(int id);
        public Task<Response<StatusDTO>> UpdateStatusAsync(int StatusId, StatusDTO StatusDTO);


    }
}