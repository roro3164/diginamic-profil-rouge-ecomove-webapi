namespace ecomove_back.Data.Models
{

    public class CarpoolAddress
    {
        public Guid CarpoolAddressId { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public CarpoolAnnouncement PickupAddressCarpool { get; set; }
        public CarpoolAnnouncement DropOffAddressCarpool { get; set; }
    }
}