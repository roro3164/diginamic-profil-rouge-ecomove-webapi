using ecomove_back.Data;

namespace ecomove_back.DTOs.VehicleDTOs
{
    public class VehicleForUpdateDTO : VehicleForCreateDTO
    {
        public int StatusId { get; set; }
    }
}
