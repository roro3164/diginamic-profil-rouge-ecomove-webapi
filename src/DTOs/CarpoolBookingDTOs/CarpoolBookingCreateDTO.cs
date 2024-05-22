using Ecomove.Api.Data.Models;

namespace Ecomove.Api.DTOs.CarpoolBookingDTOs
{
    public class CarpoolBookingCreateDTO
    {
        public bool Confirmed { get; set; }
        public Guid CarpoolAnnouncementId { get; set; }
        public string AppUserId { get; set; } = string.Empty;

    }
}
