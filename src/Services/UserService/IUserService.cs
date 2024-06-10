using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.UserService
{
    public interface IUserService
    {
        Task<Response<bool>> CreateUserAsync(CreateAppUserDTO userDTO);
        Task<Response<List<AllUsersDTO>>> GetAllUsersAysnc();
        Task<Response<UserDTO>> GetUserByIdAysnc(string id);
        Task<Response<bool>> UpdateUserAysnc(string id, UpdateUserDTO userDTO);
        Task<Response<bool>> DeleteUserAsync(string id);

    }
}
