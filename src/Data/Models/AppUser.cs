using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecomove_back.Data.Models
{
    [Index("Email", IsUnique = true)]
    public class AppUser : IdentityUser
    {
        [ForeignKey("RoleId")]
        public string RoleId { get; set; } = string.Empty;
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public IdentityRole Role { get; set; } = new();

        [PersonalData]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [PersonalData]
        [DataType(DataType.Url)]
        public string? PictureProfil { get; set; }

        [InverseProperty(nameof(AppUser))]
        public List<RentalVehicle>? RentalVehicles { get; set; }

        [InverseProperty(nameof(AppUser))]
        public List<CarpoolBooking>? CarpoolsBooking { get; set; }
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
