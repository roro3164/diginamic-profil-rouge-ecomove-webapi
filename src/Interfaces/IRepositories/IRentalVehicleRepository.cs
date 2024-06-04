using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.RentalVehicleDTO;
using ErrorOr;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IRentalVehicleRepository
    {
        Task<ErrorOr<Created>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO);
        Task<ErrorOr<List<RentalVehicle>>> GetAllRentalVehiclesAysnc(string idUserConnect);
        Task<ErrorOr<RentalVehicle>> GetRentalVehicleByIdAysnc(string idUserConnect, Guid rentalId);
        Task<ErrorOr<Updated>> UpdateRentalVehicleAsync(string idUserConnect, Guid rentalId, RentalVehicleDTO rentalVehicleDTO);
        Task<ErrorOr<Deleted>> CancelRentalVehicleAsync(string idUserConnect, Guid rentalId);
    }
}