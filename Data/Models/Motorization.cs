namespace ecomove_back.Data.Models
{
    public class Motorization
    {
        public int MotorizationId { get; set; }
        public string MotorizationLabel { get; set; }

        public List<Vehicle>? Vehicles { get; set; }
    }
}
