namespace ecomove_back.DTOs.AppUserDTOs
{
    public class AllUsersDTO
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PictureProfil { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
