using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
    // [Index("Address", IsUnique = true)]
    public class CarpoolAddress
    {
        public Guid CarpoolAddressId { get; set; }
        public string Longitude { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<CarpoolAnnouncement> PickupAddressCarpool { get; set; } = new();
        public List<CarpoolAnnouncement> DropOffAddressCarpool { get; set; } = new();
    }
}
