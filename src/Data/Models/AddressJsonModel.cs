namespace Ecomove.Api.Data.Models
{
    public class AddressJsonModel
    {
        public int place_id { get; set; }
        public string lat { get; set; } = string.Empty;
        public string lon { get; set; } = string.Empty;
        public string display_name { get; set; } = string.Empty;
    }
}