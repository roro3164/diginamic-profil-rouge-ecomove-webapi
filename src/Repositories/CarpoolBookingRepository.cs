using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CarpoolBookingDTOs;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Repositories
{
    public class CarpoolBookingRepository(EcoMoveDbContext ecoMoveDbContext) : ICarpoolBookingRepository
    {
        public async Task<ErrorOr<Created>> CreateCarpoolBookingAsync(CarpoolBookingDTO bookingDTO, string appUserId)
        {
            try
            {
                var carpoolBooking = new CarpoolBooking
                {
                    Confirmed = true,
                    CarpoolAnnouncementId = bookingDTO.CarpoolAnnouncementId,
                    AppUserId = appUserId
                };

                await ecoMoveDbContext.CarpoolBookings.AddAsync(carpoolBooking);
                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Created;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<Updated>> CancelCarpoolBookingAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            try
            {
                var carpoolBooking = await ecoMoveDbContext.CarpoolBookings
                    .Where(cb => cb.CarpoolAnnouncementId == carpoolAnnouncementId && cb.AppUserId == appUserId)
                    .FirstOrDefaultAsync();

                if (carpoolBooking == null)
                {
                    return Error.NotFound(description: $"Aucune réservation du covoiturage n'a été trouvée.");
                }

                carpoolBooking.Confirmed = false;
                ecoMoveDbContext.CarpoolBookings.Update(carpoolBooking);
                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Updated;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<List<CarpoolBookingDTO>>> GetAllCarpoolBookingsByUserIdAsync(string appUserId)
        {
            try
            {
                var carpoolBookings = await ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .Where(cb => cb.AppUserId == appUserId)
                    .ToListAsync();

                if (!carpoolBookings.Any())
                {
                    return Error.NotFound(description: "No bookings found.");
                }

                var carpoolBookingDTOs = carpoolBookings.Adapt<List<CarpoolBookingDTO>>();

                return carpoolBookingDTOs;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<List<CarpoolBookingDTO>>> GetFutureCarpoolBookingsByUserIdAsync(string appUserId)
        {
            DateTime currentDate = DateTime.Now;
            try
            {
                var carpoolBookings = await ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .Where(cb => cb.AppUserId == appUserId && cb.CarpoolAnnouncement.StartDate > currentDate)
                    .ToListAsync();

                if (!carpoolBookings.Any())
                {
                    return Error.NotFound(description: "No bookings found.");
                }

                var carpoolBookingDTOs = carpoolBookings.Adapt<List<CarpoolBookingDTO>>();

                return carpoolBookingDTOs;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<List<CarpoolBookingDTO>>> GetPastCarpoolBookingsByUserIdAsync(string appUserId)
        {
            DateTime currentDate = DateTime.Now;
            try
            {
                var carpoolBookings = await ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .Where(cb => cb.AppUserId == appUserId && cb.CarpoolAnnouncement.StartDate < currentDate)
                    .ToListAsync();
                if (!carpoolBookings.Any())
                {
                    return Error.NotFound(description: "No bookings found.");
                }

                var carpoolBookingDTOs = carpoolBookings.Adapt<List<CarpoolBookingDTO>>();

                return carpoolBookingDTOs;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<CarpoolBookingDTO>> GetCarpoolBookingByAnnouncementIdAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            try
            {
                var carpoolBooking = await ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .FirstOrDefaultAsync(cb => cb.CarpoolAnnouncementId == carpoolAnnouncementId && cb.AppUserId == appUserId);

                if (carpoolBooking == null)
                {
                    return Error.NotFound(description: "Booking not found.");
                }

                var carpoolBookingDTO = carpoolBooking.Adapt<CarpoolBookingDTO>();

                return carpoolBookingDTO;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

    }
}