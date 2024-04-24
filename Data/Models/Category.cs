namespace ecomove_back.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategroyLabel { get; set; }

        public List<Vehicle>? Vehicles { get; set; }
    }
}
