using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
        public class CarpoolBooking
        {     
                public Guid CarpoolAnnouncementId { get; set; }
                [DeleteBehavior(DeleteBehavior.NoAction)]
                public CarpoolAnnouncement CarpoolAnnouncement { get; set; }
                public string AppUserId { get; set; } = string.Empty;
                public AppUser AppUser { get; set; }
                public bool Confirmed { get; set; }
    }
}