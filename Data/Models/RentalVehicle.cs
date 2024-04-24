using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace ecomove_back.Data.Models
{
    public class RentalVehicle
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Confirmed { get; set; }
        
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
