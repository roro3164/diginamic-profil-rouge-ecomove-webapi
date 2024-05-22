using Ecomove.Api.Data;

namespace Ecomove.Api.DTOs.VehicleDTOs
{
    public class VehicleForUpdateDTO : VehicleForCreateDTO
    {
        public int StatusId { get; set; }
    }
}
