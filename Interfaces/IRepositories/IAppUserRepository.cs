using ecomove_back.Data.Models;
using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IAppUserRepository
    {
        public Task<Response<string>> CreateUserAsync(AppUserDTO userDTO);


    }
}
