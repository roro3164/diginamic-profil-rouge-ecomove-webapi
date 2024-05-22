using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CapoolAnnouncementDTOs;
using Ecomove.Api.DTOs.CarpoolAnnouncementDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface ICarpoolAnnouncementRepository
    {
        Task<Response<CarpoolAnnouncement>> CreateCarpoolAnnouncement(string userId, CarpoolAnnouncementInGoingDTO carpoolAnnouncement);
        Task<Response<List<CarpoolAnnouncementOutGoingDTO>>> GetAllCarpoolAnnouncements();
        Task<Response<CarpoolAnnouncementOutGoingDTO>> GetCarpoolAnnouncementById(Guid id);
        Task<Response<CarpoolAnnouncement>> UpdateCarpoolAnnouncement(Guid id, string userId, CarpoolAnnouncementDTO carpoolAnnouncement);
        Task<Response<CarpoolAnnouncement>> DeleteCarpoolAnnouncement(Guid id, string userId);
    }
}