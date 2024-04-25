namespace ecomove_back.Data.Models
{
    public class RentalVehicle
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Confirmed { get; set; }

        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = new();

        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = new();
    }
}
