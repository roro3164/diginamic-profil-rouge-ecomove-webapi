namespace ecomove_back.DTOs
{
    public class CarpoolAnnouncementDTO
    {

        public DateTime StartDate { get; set; }
        public double RideDuration { get; set; }
        public double RideDistance { get; set; }
        public Guid PickupAddressId { get; set; }
        public Guid DropOffAddressId { get; set; }

        public string AppUserId { get; set; } = string.Empty;
        public Guid VehicleId { get; set; }
    }
}