using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Data.Models
{
    [Index("CategoryLabel", IsUnique = true)]
    public class Category
    {
        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string CategoryLabel { get; set; } = string.Empty;

        public List<Vehicle>? Vehicles { get; set; }
    }
}
