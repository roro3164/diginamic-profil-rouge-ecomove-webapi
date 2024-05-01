using Microsoft.EntityFrameworkCore;


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
        public Guid RentalVehicleId { get; set; }
        public RentalVehicle RentalVehicle { get; set; }
    }

}