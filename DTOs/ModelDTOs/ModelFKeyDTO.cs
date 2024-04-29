using ecomove_back.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecomove_back.DTOs.ModelDTOs
{
    public class ModelFKeyDTO
    {
        [MaxLength(50)]
        public string ModelLabel { get; set; } = string.Empty;
        
        public int BrandId { get; set; }
    }
}
