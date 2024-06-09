using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.MotorizationDTOs;
using ErrorOr;


namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IMotorizationRepository
    {
        Task<ErrorOr<Created>> CreateMotorizationAsync(MotorizationDTO motorizationDTO);
        Task<ErrorOr<Deleted>> DeleteMotorizationAsync(int id);
        Task<ErrorOr<List<Motorization>>> GetAllMotorizationsAsync();
        Task<ErrorOr<Motorization>> GetMotorizationAsync(int id);
        Task<ErrorOr<Updated>> UpdateMotorizationAsync(int id, MotorizationDTO motorizationDTO);
    }
}
