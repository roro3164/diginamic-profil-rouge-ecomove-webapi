using ecomove_back.Data.Models;

namespace ecomove_back.DTOs.CarpoolAnnouncementDTOs
{
    public class CarpoolAnnouncementOutGoingDTO
    {
        public DateTime StartDate { get; set; }     
        public CarpoolAddress PickupAddress { get; set; }
        public CarpoolAddress DropOffAddress { get; set; }
    }
}
