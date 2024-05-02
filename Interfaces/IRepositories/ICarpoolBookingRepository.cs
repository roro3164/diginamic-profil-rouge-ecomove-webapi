using ecomove_back.Data.Models;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface ICarpoolBookingRepository
    {
        Task<Response<CarpoolBookingCreateDTO>> CreateCarpoolBookingAsync(CarpoolBookingCreateDTO bookingCreateDTO);
        Task<Response<string>> CancelCarpoolBookingAsync(Guid id,string appUserId);
        Task<Response<List<CarpoolBooking>>> GetAllCarpoolBookingsByUserIdAsync(string userId);
        Task<Response<List<CarpoolBooking>>> GetFutureCarpoolBookingsByUserIdAsync(string userId);
        Task<Response<List<CarpoolBooking>>> GetPastCarpoolBookingsByUserIdAsync(string userId);
        Task<Response<CarpoolBooking>> GetCarpoolBookingsByIdAsync(Guid carpoolAnnouncementId, string appUserId);
    }
}
