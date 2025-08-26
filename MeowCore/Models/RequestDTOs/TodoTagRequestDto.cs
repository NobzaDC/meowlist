using System.ComponentModel.DataAnnotations;

namespace MeowCore.Models.RequestDTOs
{
    public class TodoTagRequestDto
    {
        [Required]
        public int TodoId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}