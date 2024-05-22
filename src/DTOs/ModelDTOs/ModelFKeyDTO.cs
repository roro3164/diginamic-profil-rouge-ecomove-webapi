using System.ComponentModel.DataAnnotations;

namespace Ecomove.Api.DTOs.ModelDTOs
{
    public class ModelFKeyDTO
    {
        [MaxLength(50)]
        public string ModelLabel { get; set; } = string.Empty;

        public int BrandId { get; set; }
    }
}
