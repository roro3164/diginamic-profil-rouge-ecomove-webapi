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
        public string RoleId { get; set; } = string.Empty;
        [ForeignKey("RoleId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public IdentityRole Role { get; set; } = new();

        [InverseProperty(nameof(AppUser))]
        public List<RentalVehicle>? RentalVehicles { get; set; }

        [InverseProperty(nameof(AppUser))]
        public List<CarpoolBooking>? CarpoolsBooking { get; set; }

        public List<CarpoolAnnouncement>? CarpoolsAnnouncements { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string PictureProfil {  get; set; } = string.Empty;

        [EmailAddress]
        public override string Email { get => base.Email; set => base.Email = value; }
        
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }





    }
}
