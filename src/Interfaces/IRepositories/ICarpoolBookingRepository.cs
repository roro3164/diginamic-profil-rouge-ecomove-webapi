using ecomove_back.Data.Models;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
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
