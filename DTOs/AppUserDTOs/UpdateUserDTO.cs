using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ecomove_back.DTOs.AppUserDTOs
{
    public class UpdateUserDTO
    {
        [DefaultValue(null)]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas faire plus de 50 caractères")]
        public string? FirstName { get; set; }

        [DefaultValue(null)]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas faire plus de 50 caractères")]
        public string? LastName { get; set; }

        [DefaultValue(null)]
        public string? PictureProfil { get; set; }

        [DefaultValue(null)]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
        public string? Email { get; set; }

        [DefaultValue(null)]
        [PasswordPropertyText]
        public string? CurrentPassword { get; set; }

        [DefaultValue(null)]
        [PasswordPropertyText]
        public string? NewPassword { get; set; }
    }
}
