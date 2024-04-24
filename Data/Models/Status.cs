namespace ecomove_back.Data.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusLabel { get; set; }
        public List<Vehicle>? Vehicles { get; set; } 
    }
}