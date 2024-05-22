using Ecomove.Api.Data.Models;

namespace Ecomove.Api.DTOs.CarpoolAnnouncementDTOs
{
    public class CarpoolAnnouncementOutGoingDTO
    {
        public DateTime StartDate { get; set; }
        public CarpoolAddress PickupAddress { get; set; }
        public CarpoolAddress DropOffAddress { get; set; }
    }
}
