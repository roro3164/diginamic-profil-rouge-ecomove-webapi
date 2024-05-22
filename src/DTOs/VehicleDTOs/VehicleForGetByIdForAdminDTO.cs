using Ecomove.Api.Data;

namespace Ecomove.Api.DTOs.VehicleDTOs
{
    public class VehicleForGetByIdForAdminDTO : VehicleForGetDTO
    {
        public StatusEnum StatusLabel { get; set; }
    }
}
