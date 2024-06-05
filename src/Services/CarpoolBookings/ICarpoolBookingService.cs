using Ecomove.Api.DTOs.CarpoolBookingDTOs;
using Ecomove.Api.Helpers;
using ErrorOr;
using Ecomove.Api.Data.Models;

namespace Ecomove.Api.Services.CarpoolBookings

{
    public interface ICarpoolBookingService
    {
        Task<Response<bool>> CreateCarpoolBookingAsync(CarpoolBookingDTO bookingCreateDTO, string appUserId);
        Task<Response<List<CarpoolBookingDTO>>> GetAllCarpoolBookingsByUserIdAsync(string appUserId);
        Task<Response<List<CarpoolBookingDTO>>> GetFutureCarpoolBookingsByUserIdAsync(string appUserId);
        Task<Response<List<CarpoolBookingDTO>>> GetPastCarpoolBookingsByUserIdAsync(string appUserId);
        Task<Response<CarpoolBookingDTO>> GetCarpoolBookingByAnnouncementIdAsync(Guid carpoolAnnouncementId, string appUserId);
        Task<Response<bool>> CancelCarpoolBookingAsync(Guid carpoolAnnouncementId, string appUserId);
    }
}
