using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
    public class Model
    {
        public int ModelId { get; set; }

        [MaxLength(50)]
        public string ModelLabel { get; set; } = string.Empty;

        [ForeignKey("BrandId")]
        public int BrandId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Brand Brand { get; set; } 

        public List<Vehicle>? Vehicles { get; set; }
    }
}
