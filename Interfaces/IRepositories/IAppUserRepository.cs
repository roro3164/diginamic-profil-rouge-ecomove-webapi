using ecomove_back.DTOs.AppUserDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IAppUserRepository
    {
        public Task<Response<AppUserDTO>> CreateUserAsync(AppUserDTO userDTO);
        public string TestDate(int newStartDate, int newEndDate);

    }
}
