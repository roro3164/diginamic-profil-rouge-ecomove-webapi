namespace Ecomove.Api.Data.Models
{
    public class RentalVehicle
    {
        public Guid RentalVehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Confirmed { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; }
        public CarpoolAnnouncement CarpoolAnnouncement { get; set; }
    }
}
