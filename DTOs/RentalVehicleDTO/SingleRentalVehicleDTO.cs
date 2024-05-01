namespace ecomove_back.DTOs.RentalVehicleDTO
{
    public class SingleRentalVehicleDTO
    {
        public Guid RentalVehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
