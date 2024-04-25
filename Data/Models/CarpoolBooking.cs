namespace ecomove_back.Data.Models
{

    public class CarpoolBooking
    {
        public bool Confirmed { get; set; }

        public Guid CarpoolAnnouncementId { get; set; }
        public CarpoolAnnouncement CarpoolAnnouncement { get; set; } = new();

        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = new();
    }

}