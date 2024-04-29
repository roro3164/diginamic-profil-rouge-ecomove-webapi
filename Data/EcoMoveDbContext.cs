using ecomove_back.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data;

public class EcoMoveDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CarpoolAddress> CarpoolAddresses { get; set; }
    public DbSet<CarpoolAnnouncement> CarpoolAnnouncements { get; set; }
    public DbSet<CarpoolBooking> CarpoolBookings { get; set; }
    public DbSet<Motorization> Motorizations { get; set; }
    public DbSet<RentalVehicle> RentalVehicles { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public EcoMoveDbContext(DbContextOptions<EcoMoveDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CarpoolAnnouncement>()
                     .HasOne(m => m.PickupAddress)
                     .WithMany(p => p.PickupAddressCarpool)
                     .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<CarpoolAnnouncement>()
            .HasOne(m => m.DropOffAddress)
            .WithMany(p => p.DropOffAddressCarpool)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<RentalVehicle>()
            .HasKey(rv => new { rv.VehicleId, rv.AppUserId });

        builder.Entity<CarpoolBooking>()
            .HasKey(rv => new { rv.CarpoolAnnouncementId, rv.AppUserId });

        builder.Entity<AppUser>()
             .HasOne(e => e.Role)  // Un utilisateur a un seul rôle
             .WithMany()           // Un rôle peut être lié à plusieurs utilisateurs
             .HasForeignKey(e => e.RoleId);  // Clé étrangère dans AppUser

        // Permet de créer les deux rôles en BDD
        IdentityRole roleAdmin = new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" };
        IdentityRole roleUser = new IdentityRole { Name = "USER", NormalizedName = "USER" };
        builder.Entity<IdentityRole>().HasData(new List<IdentityRole> { roleAdmin, roleUser });

        base.OnModelCreating(builder);
    }
}
