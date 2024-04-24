using ecomove_back.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace ecomove_back.Data;

public class EcoMoveDbContext : IdentityDbContext<AppUser>
{
    DbSet<Brand> Brands { get; set; }
    DbSet<CarpoolAddress> CarpoolAddresses { get; set; }
    DbSet<CarpoolAnnouncement> CarpoolAnnouncements { get; set; }
    DbSet<CarpoolBooking> CarpoolBookings { get; set; }
    DbSet<Motorization> Motorizations { get; set; }
    DbSet<RentalVehicle> RentalVehicles { get; set; }
    DbSet<Model> Models { get; set; }

    public EcoMoveDbContext(DbContextOptions<EcoMoveDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CarpoolAnnouncement>()
                    .HasOne(m => m.PickupAddress)
                    .WithOne(p => p.PickupAddressCarpool)
                    .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<CarpoolAnnouncement>()
            .HasOne(m => m.DropOffAddress)
            .WithOne(p => p.DropOffAddressCarpool)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<RentalVehicle>()
            .HasKey(rv => new { rv.VehicleId, rv.AppUserId });

        builder.Entity<CarpoolBooking>()
            .HasKey(rv => new { rv.CarpoolAnnouncementId, rv.AppUserId });

        builder.Entity<AppUser>()
             .HasOne(e => e.Role)  // Un utilisateur a un seul rôle
             .WithMany()           // Un rôle peut être lié à plusieurs utilisateurs
             .HasForeignKey(e => e.RoleId);  // Clé étrangère dans AppUser

        base.OnModelCreating(builder);
    }
}
