namespace ecomove_back.Data.Models
{

        public class CarpoolBooking
        {
                public Guid CarpoolAnnouncementId { get; set; }
                public bool Confirmed { get; set; }


                public CarpoolAnnouncement CarpoolAnnouncement { get; set; }

                public string AppUserId { get; set; } = string.Empty;
                public AppUser AppUser { get; set; }
        }
}