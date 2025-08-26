using System.ComponentModel.DataAnnotations;

namespace MeowCore.Models.RequestDTOs
{
    public class UserRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        public bool IsAdmin { get; set; } = false;
    }
}