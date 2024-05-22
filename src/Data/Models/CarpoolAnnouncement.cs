using Microsoft.EntityFrameworkCore;


namespace Ecomove.Api.Data.Models
{
    public class CarpoolAnnouncement
    {
        public Guid CarpoolAnnouncementId { get; set; }
        public DateTime StartDate { get; set; }
        public double RideDuration { get; set; }
        public double RideDistance { get; set; }
        public Guid PickupAddressId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CarpoolAddress PickupAddress { get; set; }
        public Guid DropOffAddressId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CarpoolAddress DropOffAddress { get; set; }
        public Guid RentalVehicleId { get; set; }
        public RentalVehicle RentalVehicle { get; set; }
        public List<CarpoolBooking>? Bookings { get; set; }
    }

}