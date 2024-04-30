using ecomove_back.Data.Models;
using ecomove_back.DTOs;
using ecomove_back.Helpers;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface ICarpoolAnnouncementRepository
    {
        Task<Response<CarpoolAnnouncement>> CreateCarpoolAnnouncement(CarpoolAnnouncementDTO carpoolAnnouncement);
        Task<Response<List<CarpoolAnnouncement>>> GetAllCarpoolAnnouncements();
        Task<Response<CarpoolAnnouncement>> GetCarpoolAnnouncementById(Guid id);
        Task<Response<CarpoolAnnouncement>> UpdateCarpoolAnnouncement(Guid id, CarpoolAnnouncementDTO carpoolAnnouncement);
        Task<Response<CarpoolAnnouncement>> DeleteCarpoolAnnouncement(Guid id);
    }
}