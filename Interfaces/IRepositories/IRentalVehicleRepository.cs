using ecomove_back.DTOs.RentalVehicleDTO;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface IRentalVehicleRepository
    {
        public Task<Response<string>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO);
        public Task<Response<RentalVehicleDTO>> UpdateRentalVehicleAsync(int rentalId, RentalVehicleDTO userDTO);
        public Task<Response<string>> CancelRentalVehicleAsync(int rentalId);
        public Task<Response<List<AllRentalVehicles>>> GetAllRentalVehiclesAysnc();
        public Task<Response<SingleRentalVehicleDTO>> GetRentalVehicleByIdAysnc(int rentalId);
    }
}