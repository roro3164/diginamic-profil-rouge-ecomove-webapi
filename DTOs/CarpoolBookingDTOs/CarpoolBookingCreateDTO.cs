using ecomove_back.Data.Models;

namespace ecomove_back.DTOs.CarpoolBookingDTOs
{
    public class CarpoolBookingCreateDTO
    {
        public bool Confirmed { get; set; }
        public Guid CarpoolAnnouncementId { get; set; }
        public string AppUserId { get; set; } = string.Empty;

    }
}
