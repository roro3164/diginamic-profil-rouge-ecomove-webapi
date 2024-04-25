using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
    [Index("StatusLabel", IsUnique = true)]
    public class Status
    {
        public int StatusId { get; set; }

        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum StatusLabel { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
    }
}