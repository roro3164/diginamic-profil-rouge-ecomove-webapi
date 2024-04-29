using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.DTOs.BrandDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
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
