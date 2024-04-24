﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ecomove_back.Data;

#nullable disable

namespace ecomove_back.Migrations
{
    [DbContext(typeof(EcoMoveDbContext))]
    [Migration("20240424135808_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ecomove_back.Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.CarpoolAddress", b =>
                {
                    b.Property<Guid>("CarpoolAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("CarpoolAddressId");

                    b.ToTable("CarpoolAddresses");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.CarpoolAnnouncement", b =>
                {
                    b.Property<Guid>("CarpoolAnnouncementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("DropOffAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PickupAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("RideDistance")
                        .HasColumnType("float");

                    b.Property<double>("RideDuration")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CarpoolAnnouncementId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("DropOffAddressId")
                        .IsUnique();

                    b.HasIndex("PickupAddressId")
                        .IsUnique();

                    b.HasIndex("VehicleId");

                    b.ToTable("CarpoolAnnouncements");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.CarpoolBooking", b =>
                {
                    b.Property<Guid>("CarpoolAnnouncementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.HasKey("CarpoolAnnouncementId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("CarpoolBookings");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategroyLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("ModelLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModelId");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Motorization", b =>
                {
                    b.Property<int>("MotorizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MotorizationId"));

                    b.Property<string>("MotorizationLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MotorizationId");

                    b.ToTable("Motorizations");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.RentalVehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VehicleId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("RentalVehicles");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("C02emission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarSeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("MotorizationId")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ModelId");

                    b.HasIndex("MotorizationId");

                    b.HasIndex("StatusId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ecomove_back.Data.Models.AppUser", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.CarpoolAnnouncement", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.AppUser", "AppUser")
                        .WithMany("CarpoolsAnnouncements")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.CarpoolAddress", "DropOffAddress")
                        .WithOne("DropOffAddressCarpool")
                        .HasForeignKey("ecomove_back.Data.Models.CarpoolAnnouncement", "DropOffAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.CarpoolAddress", "PickupAddress")
                        .WithOne("PickupAddressCarpool")
                        .HasForeignKey("ecomove_back.Data.Models.CarpoolAnnouncement", "PickupAddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.Vehicle", "Vehicle")
                        .WithMany("CarpoolAnnouncements")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("DropOffAddress");

                    b.Navigation("PickupAddress");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.CarpoolBooking", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.AppUser", "AppUser")
                        .WithMany("CarpoolsBooking")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.CarpoolAnnouncement", "CarpoolAnnouncement")
                        .WithMany()
                        .HasForeignKey("CarpoolAnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("CarpoolAnnouncement");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Model", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.RentalVehicle", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.AppUser", "AppUser")
                        .WithMany("RentalVehicles")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.Vehicle", "Vehicle")
                        .WithMany("RentalVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Vehicle", b =>
                {
                    b.HasOne("ecomove_back.Data.Models.Category", "Category")
                        .WithMany("Vehicles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.Model", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.Motorization", "Motorization")
                        .WithMany("Vehicles")
                        .HasForeignKey("MotorizationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ecomove_back.Data.Models.Status", "Status")
                        .WithMany("Vehicles")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Model");

                    b.Navigation("Motorization");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.AppUser", b =>
                {
                    b.Navigation("CarpoolsAnnouncements");

                    b.Navigation("CarpoolsBooking");

                    b.Navigation("RentalVehicles");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Brand", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.CarpoolAddress", b =>
                {
                    b.Navigation("DropOffAddressCarpool")
                        .IsRequired();

                    b.Navigation("PickupAddressCarpool")
                        .IsRequired();
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Category", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Model", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Motorization", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Status", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("ecomove_back.Data.Models.Vehicle", b =>
                {
                    b.Navigation("CarpoolAnnouncements");

                    b.Navigation("RentalVehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
