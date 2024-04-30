using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;

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
                    AppUserId = carpoolAnnouncementDTO.AppUserId,
                    VehicleId = carpoolAnnouncementDTO.VehicleId,
                    PickupAddressId = carpoolAnnouncementDTO.PickupAddressId,
                    DropOffAddressId = carpoolAnnouncementDTO.DropOffAddressId,
                    StartDate = carpoolAnnouncementDTO.StartDate,
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

        public Task<Response<CarpoolAnnouncement>> DeleteCarpoolAnnouncement(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<CarpoolAnnouncement>>> GetAllCarpoolAnnouncements()
        {
            throw new NotImplementedException();
        }

        public Task<Response<CarpoolAnnouncement>> GetCarpoolAnnouncementById()
        {
            throw new NotImplementedException();
        }

        public Task<Response<CarpoolAnnouncement>> UpdateCarpoolAnnouncement(Guid id, CarpoolAnnouncementDTO carpoolAnnouncement)
        {
            throw new NotImplementedException();
        }
    }
}