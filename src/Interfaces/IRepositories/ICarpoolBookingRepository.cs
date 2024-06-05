using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CarpoolBookingDTOs;
using ErrorOr;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface ICarpoolBookingRepository
    {
        Task<ErrorOr<Created>> CreateCarpoolBookingAsync(CarpoolBookingDTO bookingDTO, string appUserId);
        Task<ErrorOr<Updated>> CancelCarpoolBookingAsync(Guid carpoolAnnouncementId, string appUserId);
        Task<ErrorOr<List<CarpoolBookingDTO>>> GetAllCarpoolBookingsByUserIdAsync(string appUserId);
        Task<ErrorOr<List<CarpoolBookingDTO>>> GetFutureCarpoolBookingsByUserIdAsync(string appUserId);
        Task<ErrorOr<List<CarpoolBookingDTO>>> GetPastCarpoolBookingsByUserIdAsync(string appUserId);
        Task<ErrorOr<CarpoolBookingDTO>> GetCarpoolBookingByAnnouncementIdAsync(Guid carpoolAnnouncementId, string appUserId);
    }
}