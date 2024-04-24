using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Data.Models
{
    public class Model
    {
        public int ModelId { get; set; }   
        public string ModelLabel { get; set; }

        public int BrandId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Brand Brand { get; set; }

        public List<Vehicle>? Vehicles { get; set; }
    }
}
