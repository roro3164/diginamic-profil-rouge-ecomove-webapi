using ecomove_back.Data.Models;

namespace ecomove_back.DTOs.RentalVehicleDTO
{
    public class AllRentalVehicles
    {
        public Guid RentalVehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public Guid VehicleId { get; set; }
        //public Vehicle Vehicle { get; set; }

        //public string AppUserId { get; set; } = string.Empty;
        //public AppUser AppUser { get; set; }
    }
}
