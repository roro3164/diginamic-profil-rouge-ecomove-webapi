using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.DTOs.RentalVehicleDTO;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.RentalVehicles
{
    public interface IRentalVehicleService
    {
        Task<Response<bool>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO);
        Task<Response<List<AllRentalVehicles>>> GetAllRentalVehiclesAysnc(string idUserConnect);
        Task<Response<SingleRentalVehicleDTO>> GetRentalVehicleByIdAysnc(string idUserConnect, Guid rentalId);
        Task<Response<bool>> UpdateRentalVehicleAsync(string idUserConnect, Guid rentalId, RentalVehicleDTO userDTO);
        Task<Response<bool>> CancelRentalVehicleAsync(string idUserConnect, Guid rentalId);
    }
}
