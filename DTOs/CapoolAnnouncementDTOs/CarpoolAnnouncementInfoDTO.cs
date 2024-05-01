namespace ecomove_back.DTOs.CapoolAnnouncementDTOs
{
    public class CarpoolAnnouncementInfoDTO
    {
        public DateTime StartDate { get; set; }
        public Guid PickupAddressId { get; set; }
        public Guid DropOffAddressId { get; set; }
    }
}
