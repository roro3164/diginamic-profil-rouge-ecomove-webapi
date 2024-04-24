namespace ecomove_back.Data.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandLabel { get; set; }

        public List<Model>? Models { get; set; }
    }
}
