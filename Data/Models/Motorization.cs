using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{

    [Index("MotorizationLabel", IsUnique = true)]

    public class Motorization
    {
        public int MotorizationId { get; set; }

        [MaxLength(50)]
        public string MotorizationLabel { get; set; } = string.Empty;

        public List<Vehicle>? Vehicles { get; set; }
    }
}
