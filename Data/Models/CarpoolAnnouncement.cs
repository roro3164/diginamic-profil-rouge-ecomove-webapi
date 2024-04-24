using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecomove_back.Data.Models
{


    public class CarpoolAnnouncement
    {
        public Guid CarpoolAnnouncementId { get; set; }
        public DateTime StartDate { get; set; }
        public double RideDuration { get; set; }
        public double RideDistance { get; set; }

        public Guid PickupAddressId { get; set; }
        public CarpoolAddress PickupAddress { get; set; }

        public Guid DropOffAddressId { get; set; }
        public CarpoolAddress DropOffAddress { get; set; }

        public string AppUserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public AppUser AppUser { get; set; }

        public Guid VehicleId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Vehicle Vehicle { get; set; }

    }

}