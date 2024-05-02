using ecomove_back.Data.Models;
using ecomove_back.DTOs.CapoolAnnouncementDTOs;
using ecomove_back.DTOs.CarpoolAnnouncementDTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface ICarpoolAnnouncementRepository
    {
        Task<Response<CarpoolAnnouncement>> CreateCarpoolAnnouncement(string userId, CarpoolAnnouncementInGoingDTO carpoolAnnouncement);
        Task<Response<List<CarpoolAnnouncementOutGoingDTO>>> GetAllCarpoolAnnouncements();
        Task<Response<CarpoolAnnouncementOutGoingDTO>> GetCarpoolAnnouncementById(Guid id);
        Task<Response<CarpoolAnnouncement>> UpdateCarpoolAnnouncement(Guid id, string userId, CarpoolAnnouncementInGoingDTO carpoolAnnouncement);
        Task<Response<CarpoolAnnouncement>> DeleteCarpoolAnnouncement(Guid id, string userId);
    }
}