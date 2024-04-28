using System.ComponentModel.DataAnnotations;

namespace ecomove_back.DTOs.AppUserDTOs
{
    public class AppUserDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Vous devez entrer votre prénom")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Vous devez entrer votre nom")]
        public string LastName { get; set; } = string.Empty;

        public string? PictureProfil { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
