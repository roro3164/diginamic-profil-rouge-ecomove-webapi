using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.CapoolAnnouncementDTOs;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.DTOs.ModelDTOs;
using ecomove_back.DTOs.MotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using EntityFramework.Exceptions.Common;
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
                var booking = new CarpoolBooking
                {
                    Confirmed = bookingCreateDTO.Confirmed,
                    CarpoolAnnouncementId = bookingCreateDTO.CarpoolAnnouncementId,
                    AppUserId = bookingCreateDTO.AppUserId
                };

                await _ecoMoveDbContext.CarpoolBookings.AddAsync(booking);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolBookingCreateDTO>
                {
                    Message = "Covoiturage réservé avec succès",
                    Data = new CarpoolBookingCreateDTO
                    {
                        Confirmed = booking.Confirmed,
                        CarpoolAnnouncementId = booking.CarpoolAnnouncementId,
                        AppUserId = booking.AppUserId
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
        public async Task<Response<string>> CancelCarpoolBookingAsync(int id)
        {
            var booking = await _ecoMoveDbContext.CarpoolBookings.FindAsync(id);

            if (booking == null)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = "Réservation introuvable",
                    CodeStatus = 404
                };
            }

            booking.Confirmed = false;

            try
            {
                _ecoMoveDbContext.CarpoolBookings.Update(booking);
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

   



        //VIENDO LISTAS

        //    public async Task<List<CarpoolBooking>> GetCarpoolBookingsByUserIdAsync(string userId)
        //    {
        //        return await _context.CarpoolBookings
        //            .Where(cb => cb.AppUserId == userId)
        //            .ToListAsync();
        //    }

        //    public async Task<List<CarpoolBooking>> GetFutureCarpoolBookingsByUserIdAsync(string userId)
        //    {
        //        if (carpoolBokings.Count > 0)
        //        {
        //            foreach (CarpoolBooking carpool in carpoolBokings)
        //            {
        //                motorizationDTOs.Add(new CarpoolBooking { AppUserId = carpoolBoking.AppUserId });
        //            }

        //            return new Response<List<MotorizationDTO>>
        //            {
        //                IsSuccess = true,
        //                Data = motorizationDTOs,
        //                Message = null,
        //                CodeStatus = 201,
        //            };
        //        }

        //        if (futureBookings.Count == 0)
        //        {
        //            return new Response<List<MotorizationDTO>>
        //            {
        //                IsSuccess = false,
        //                Message = "La liste des motorisations est vide",
        //                CodeStatus = 404
        //            };
        //        }
        //        else
        //        {
        //            return new Response<List<MotorizationDTO>>
        //            {
        //                IsSuccess = false,
        //            };
        //        }

        //        DateTime currentDate = DateTime.Now;
        //        return await _context.CarpoolBookings
        //            .Where(cb => cb.AppUserId == userId && cb.CarpoolAnnouncement.Date > currentDate)
        //            .ToListAsync();
        //    }

        //    public async Task<List<CarpoolBooking>> GetPastCarpoolBookingsByUserIdAsync(string userId)
        //    {
        //        DateTime currentDate = DateTime.Now;
        //        return await _context.CarpoolBookings
        //            .Where(cb => cb.AppUserId == userId && cb.CarpoolAnnouncement.Date < currentDate)
        //            .ToListAsync();
        //    }
        //}

      

    }
}