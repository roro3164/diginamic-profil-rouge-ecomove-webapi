using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecomove_back.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string RoleId { get; set; } = string.Empty;
        [ForeignKey("RoleId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public IdentityRole Role { get; set; } = new();

        [InverseProperty(nameof(AppUser))]
        public List<RentalVehicle>? RentalVehicles { get; set; }

        [InverseProperty(nameof(AppUser))]
        public List<CarpoolBooking>? CarpoolsBooking { get; set; }

        public List<CarpoolAnnouncement>? CarpoolsAnnouncements { get; set; }
    }
}
