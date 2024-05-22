using Ecomove.Api.Data.Models;

namespace Ecomove.Api.DTOs.RentalVehicleDTO
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
