using System.ComponentModel.DataAnnotations;

namespace ecomove_back.DTOs.RentalVehicleDTO
{
    public class CreateRentalVehicleDTO
    {
        [Required(ErrorMessage = "Vous devez entrer une date de départ")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Vous devez entrer une date de fin")]
        public DateTime EndDate { get; set; }

    }
}
