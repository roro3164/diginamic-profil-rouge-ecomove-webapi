using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class CarpoolAnnouncementRepository : ICarpoolAnnouncementRepository
    {
        private readonly EcoMoveDbContext _ecoMoveDbContext;

        public CarpoolAnnouncementRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;

        }

        public async Task<Response<CarpoolAnnouncement>> CreateCarpoolAnnouncement(CarpoolAnnouncementDTO carpoolAnnouncementDTO)
        {
            try
            {
                CarpoolAnnouncement carpoolAnnouncement = new CarpoolAnnouncement
                {
                    PickupAddressId = carpoolAnnouncementDTO.PickupAddressId,
                    DropOffAddressId = carpoolAnnouncementDTO.DropOffAddressId,
                    StartDate = DateTime.Now,
                    RideDuration = carpoolAnnouncementDTO.RideDuration,
                    RideDistance = carpoolAnnouncementDTO.RideDistance,
                };

                _ecoMoveDbContext.CarpoolAnnouncements.Add(carpoolAnnouncement);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = "L'annonce de covoiturage a bien été créée",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };

            }
        }

        public async Task<Response<CarpoolAnnouncement>> DeleteCarpoolAnnouncement(Guid id)
        {
            try
            {
                CarpoolAnnouncement? carpoolAnnouncement = await _ecoMoveDbContext.CarpoolAnnouncements.FindAsync(id);

                if (carpoolAnnouncement is null)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        Message = "Aucune annonce de covoiturage n'a été trouvée",
                        IsSuccess = false,
                        CodeStatus = 404
                    };

                _ecoMoveDbContext.CarpoolAnnouncements.Remove(carpoolAnnouncement);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = "L'annonce du covoiturage a été supprimée avec succès",
                    IsSuccess = true,
                    CodeStatus = 204
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            };

        }

        public async Task<Response<List<CarpoolAnnouncement>>> GetAllCarpoolAnnouncements()
        {
            try
            {
                List<CarpoolAnnouncement> carpoolAnnouncements = await _ecoMoveDbContext.CarpoolAnnouncements
                .Include(c => c.PickupAddress)
                .Include(c => c.DropOffAddress)
                .ToListAsync();

                if (carpoolAnnouncements.Count > 0)
                    return new Response<List<CarpoolAnnouncement>>()
                    {
                        Data = carpoolAnnouncements,
                        IsSuccess = true,
                        CodeStatus = 200,
                        Message = null
                    };


                return new Response<List<CarpoolAnnouncement>>
                {
                    Data = null,
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune annonce de covoiturage n'a été trouvée"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CarpoolAnnouncement>>
                {
                    Data = null,
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<CarpoolAnnouncement>> GetCarpoolAnnouncementById(Guid id)
        {
            try
            {
                CarpoolAnnouncement? carpoolAnnouncement = await _ecoMoveDbContext.CarpoolAnnouncements.FindAsync(id);

                if (carpoolAnnouncement is null)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        Message = "Aucune annonce de covoiturage n'a été trouvée",
                        IsSuccess = false,
                        CodeStatus = 404
                    };

                return new Response<CarpoolAnnouncement>
                {
                    Data = carpoolAnnouncement,
                    Message = null,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            };
        }

        public async Task<Response<CarpoolAnnouncement>> UpdateCarpoolAnnouncement(Guid id, CarpoolAnnouncementDTO carpoolAnnouncementDTO)
        {
            try
            {
                CarpoolAnnouncement? carpoolAnnouncement = await _ecoMoveDbContext.CarpoolAnnouncements.FindAsync(id);

                if (carpoolAnnouncement is null)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune annonce covoiturage n'a été trouvée"
                    };

                carpoolAnnouncement.StartDate = carpoolAnnouncementDTO.StartDate;

                carpoolAnnouncement.PickupAddress = carpoolAnnouncement.PickupAddress;
                carpoolAnnouncement.DropOffAddress = carpoolAnnouncement.DropOffAddress;
                carpoolAnnouncement.RideDuration = carpoolAnnouncementDTO.RideDuration;
                carpoolAnnouncement.RideDistance = carpoolAnnouncementDTO.RideDistance;


                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = "L'annonce a été bien mise à jour avec succès",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }
    }
}