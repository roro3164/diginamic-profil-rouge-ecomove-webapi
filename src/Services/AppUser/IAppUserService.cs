using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.AppUser
{
    public interface IAppUserService
    {
        Task<Response<bool>> CreateUserAsync(CreateAppUserDTO userDTO);
        Task<Response<List<AllUsersDTO>>> GetAllUsersAysnc();
        Task<Response<UserDTO>> GetUserByIdAysnc(string id);
        Task<Response<bool>> UpdateUserAysnc(string id, UpdateUserDTO userDTO);
        Task<Response<bool>> DeleteUserAsync(string id);
    }
}
