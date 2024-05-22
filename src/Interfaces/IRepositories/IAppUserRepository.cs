using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IAppUserRepository
    {
        public Task<Response<string>> CreateUserAsync(CreateAppUserDTO userDTO);
        public Task<Response<List<AllUsersDTO>>> GetAllUsersAysnc();
        public Task<Response<UserDTO>> GetUserByIdAysnc(string userId);
        public Task<Response<UpdateUserDTO>> UpdateUserAysnc(string userId, UpdateUserDTO userDTO);
        public Task<Response<string>> DeleteUserAsync(string userId);
    }
}
