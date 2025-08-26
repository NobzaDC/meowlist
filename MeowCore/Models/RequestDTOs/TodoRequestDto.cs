using System.ComponentModel.DataAnnotations;
using MeowCore.Helpers;

namespace MeowCore.Models.RequestDTOs
{
    public class TodoRequestDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TodoStatus Status { get; set; } = TodoStatus.Pending;

        [Required]
        public int ListId { get; set; }
    }
}