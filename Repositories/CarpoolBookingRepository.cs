using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class CarpoolBookingRepository : ICarpoolBookingRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;
        public CarpoolBookingRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }
        public async Task<Response<CarpoolBookingCreateDTO>> CreateCarpoolBookingAsync(CarpoolBookingCreateDTO bookingCreateDTO)
        {
            try
            {
                var carpoolbooking = new CarpoolBooking
                {
                    Confirmed = bookingCreateDTO.Confirmed,
                    CarpoolAnnouncementId = bookingCreateDTO.CarpoolAnnouncementId,
                    AppUserId = bookingCreateDTO.AppUserId
                };

                await _ecoMoveDbContext.CarpoolBookings.AddAsync(carpoolbooking);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolBookingCreateDTO>
                {
                    Message = "Covoiturage réservé avec succès",
                    Data = new CarpoolBookingCreateDTO
                    {
                        Confirmed = carpoolbooking.Confirmed,
                        CarpoolAnnouncementId = carpoolbooking.CarpoolAnnouncementId,
                        AppUserId = carpoolbooking.AppUserId
                    },
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<CarpoolBookingCreateDTO>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }
        public async Task<Response<string>> CancelCarpoolBookingAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            var carpoolBooking = _ecoMoveDbContext.CarpoolBookings
                .Where(cb => cb.CarpoolAnnouncementId == carpoolAnnouncementId && cb.AppUserId == appUserId)
                .FirstOrDefault();

            if (carpoolBooking == null)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = "Réservation introuvable",
                    CodeStatus = 404
                };
            }

            carpoolBooking.Confirmed = false;

            try
            {
                _ecoMoveDbContext.CarpoolBookings.Update(carpoolBooking);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "Réservation annulée avec succès"
                };
            }
            catch (Exception e)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
        public async Task<Response<List<CarpoolBooking>>> GetAllCarpoolBookingsByUserIdAsync(string userId)
        {
            try
            {
                List<CarpoolBooking> carpoolBookings = await _ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .Where(cb => cb.AppUserId == userId)
                    .ToListAsync();

                if (carpoolBookings == null || carpoolBookings.Count == 0)
                {
                    return new Response<List<CarpoolBooking>>
                    {
                        Data = null,
                        Message = "Aucun covoiturage n'a été trouvé",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                return new Response<List<CarpoolBooking>>
                {
                    Data = carpoolBookings,
                    Message = "Covoiturages trouvés avec succès",
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<List<CarpoolBooking>>
                {
                    Data = null,
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }
        public async Task<Response<List<CarpoolBooking>>> GetFutureCarpoolBookingsByUserIdAsync(string userId)
        {
            DateTime currentDate = DateTime.Now;
            try
            {
                List<CarpoolBooking> carpoolBookings = await _ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .Where(cb => cb.AppUserId == userId && cb.CarpoolAnnouncement.StartDate > currentDate)
                    .ToListAsync();

                if (carpoolBookings == null || carpoolBookings.Count == 0)
                {
                    return new Response<List<CarpoolBooking>>
                    {
                        Data = null,
                        Message = "Aucun covoiturage n'a été trouvé",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                return new Response<List<CarpoolBooking>>
                {
                    Data = carpoolBookings,
                    Message = "Covoiturages trouvés avec succès",
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<List<CarpoolBooking>>
                {
                    Data = null,
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }
        public async Task<Response<List<CarpoolBooking>>> GetPastCarpoolBookingsByUserIdAsync(string userId)
        {
            DateTime currentDate = DateTime.Now;
            try
            {
                List<CarpoolBooking> carpoolBookings = await _ecoMoveDbContext.CarpoolBookings
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.PickupAddress)
                    .Include(cb => cb.CarpoolAnnouncement)
                        .ThenInclude(ca => ca.DropOffAddress)
                    .Where(cb => cb.AppUserId == userId && cb.CarpoolAnnouncement.StartDate < currentDate)
                    .ToListAsync();

                if (carpoolBookings == null || carpoolBookings.Count == 0)
                {
                    return new Response<List<CarpoolBooking>>
                    {
                        Data = null,
                        Message = "Aucun covoiturage n'a été trouvé",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                return new Response<List<CarpoolBooking>>
                {
                    Data = carpoolBookings,
                    Message = "Covoiturages trouvés avec succès",
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<List<CarpoolBooking>>
                {
                    Data = null,
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }
        public async Task<Response<CarpoolBooking>> GetCarpoolBookingsByIdAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            var carpoolBooking = await _ecoMoveDbContext.CarpoolBookings
                .Where(cb => cb.CarpoolAnnouncementId == carpoolAnnouncementId && cb.AppUserId == appUserId)
                .FirstOrDefaultAsync();

            try
            {
                if (carpoolBooking == null)
                {
                    return new Response<CarpoolBooking>
                    {
                        Data = null,
                        Message = "Aucune annonce de covoiturage n'a été trouvée",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                return new Response<CarpoolBooking>
                {
                    Data = carpoolBooking,
                    Message = null,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<CarpoolBooking>
                {
                    Data = null,
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }
    }
}


