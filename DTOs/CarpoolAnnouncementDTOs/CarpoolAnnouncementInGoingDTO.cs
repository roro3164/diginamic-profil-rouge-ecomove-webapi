namespace ecomove_back.DTOs.CapoolAnnouncementDTOs
{
    public class CarpoolAnnouncementInGoingDTO
    {
        public DateTime StartDate { get; set; }
        public Guid RentalVehicleId { get; set; }
        public Guid PickupAddressId { get; set; }
        public Guid DropOffAddressId { get; set; }
    }
}
