using Ecomove.Api.DTOs.RentalVehicleDTO;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IRentalVehicleRepository
    {
        public Task<Response<string>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO);
        public Task<Response<RentalVehicleDTO>> UpdateRentalVehicleAsync(string idUserConnect, Guid rentalId, RentalVehicleDTO userDTO);
        public Task<Response<string>> CancelRentalVehicleAsync(string idUserConnect, Guid rentalId);
        public Task<Response<List<AllRentalVehicles>>> GetAllRentalVehiclesAysnc(string idUserConnect);
        public Task<Response<SingleRentalVehicleDTO>> GetRentalVehicleByIdAysnc(string idUserConnect, Guid rentalId);

    }
}