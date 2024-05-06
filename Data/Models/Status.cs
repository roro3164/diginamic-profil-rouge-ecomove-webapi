using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
    [Index("StatusLabel", IsUnique = true)]
    public class Status
    {
        public int StatusId { get; set; }
        public StatusEnum StatusLabel { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
    }
}