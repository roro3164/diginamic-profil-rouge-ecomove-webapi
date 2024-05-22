using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CarpoolBookingDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface ICarpoolBookingRepository
    {
        Task<Response<CarpoolBookingCreateDTO>> CreateCarpoolBookingAsync(CarpoolBookingCreateDTO bookingCreateDTO, string? idUserConnect);
        Task<Response<string>> CancelCarpoolBookingAsync(Guid id);
        Task<Response<List<CarpoolBooking>>> GetAllCarpoolBookingsByUserIdAsync(string idUserConnect);
        Task<Response<List<CarpoolBooking>>> GetFutureCarpoolBookingsByUserIdAsync(string idUserConnect);
        Task<Response<List<CarpoolBooking>>> GetPastCarpoolBookingsByUserIdAsync(string idUserConnect);
        Task<Response<CarpoolBooking>> GetCarpoolBookingsByIdAsync(Guid id);
    }
}
