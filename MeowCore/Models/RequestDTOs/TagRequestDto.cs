using System.ComponentModel.DataAnnotations;

namespace MeowCore.Models.RequestDTOs
{
    public class TagRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Color { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
    }
}