using System.ComponentModel.DataAnnotations;

namespace MeowCore.Models.RequestDTOs
{
    public class ListRequestDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
    }
}