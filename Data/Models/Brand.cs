using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{

    [Index("BrandLabel", IsUnique = true)]
    public class Brand
    {
        public int BrandId { get; set; }

        [MaxLength(50)]
        public string BrandLabel { get; set; } = string.Empty;

        public List<Model> Models { get; set; }
    }
}
