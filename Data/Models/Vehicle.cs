using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ecomove_back.Data.Models
{
    [Index("Registration", IsUnique = true)]
    public class Vehicle
    {
        public Guid VehicleId { get; set; }

        [Range(2, 8)]
        public int CarSeatNumber { get; set; }

        [MaxLength(10)]
        public string Registration { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string Photo { get; set; } = string.Empty;
      
        public int CO2emission { get; set; } 

        public double Consumption { get; set; }

        public int StatusId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]

        public Status Status { get; set; }
        public int CategoryId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Category Category { get; set; }

        public int MotorizationId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Motorization Motorization { get; set; }

        public int ModelId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Model Model { get; set; }

        public List<RentalVehicle>? RentalVehicles { get; set; }

        public List<CarpoolAnnouncement>? CarpoolAnnouncements { get; set; }
    }
}
