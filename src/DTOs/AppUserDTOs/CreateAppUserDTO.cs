using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecomove.Api.DTOs.AppUserDTOs
{
    public class CreateAppUserDTO
    {
        [Required(ErrorMessage = "Vous devez entrer le prénom")]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas faire plus de 50 caractères")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vous devez entrer le nom")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas faire plus de 50 caractères")]
        public string LastName { get; set; } = string.Empty;

        [DefaultValue(null)]
        public string? PictureProfil { get; set; }

        [Required(ErrorMessage = "Vous devez entrer l'adresse email")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vous devez entrer le mot de passe")]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
