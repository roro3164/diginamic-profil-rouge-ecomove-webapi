using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecomove_back.Data.Models
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public int CarSeatNumber { get; set; }
        public string Registration { get; set; }
        public string Photo { get; set; }
        public string C02emission { get; set; }

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
