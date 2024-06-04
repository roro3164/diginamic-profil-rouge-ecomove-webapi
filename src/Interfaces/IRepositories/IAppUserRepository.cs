using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.Helpers;
using ErrorOr;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IAppUserRepository
    {
        Task<ErrorOr<Created>> CreateUserAsync(CreateAppUserDTO userDTO);
        Task<ErrorOr<List<AppUser>>> GetAllUsersAysnc();
        Task<ErrorOr<AppUser>> GetUserByIdAysnc(string id);
        Task<ErrorOr<Updated>> UpdateUserAysnc(string id, UpdateUserDTO userDTO);
        Task<ErrorOr<Deleted>> DeleteUserAsync(string id);
    }
}
