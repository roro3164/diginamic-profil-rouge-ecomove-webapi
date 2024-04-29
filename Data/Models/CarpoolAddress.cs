using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
    [Index("Address", IsUnique = true)]
    public class CarpoolAddress
    {
        public Guid CarpoolAddressId { get; set; }
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<CarpoolAnnouncement> PickupAddressCarpool { get; set; } = new();
        public List<CarpoolAnnouncement> DropOffAddressCarpool { get; set; } = new();
    }
}
