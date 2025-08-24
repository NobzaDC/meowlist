using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeowCore.Models
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Color { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Lists User { get; set; } = null!;

        // Relaciones
        public ICollection<TodosTags> TodoTags { get; set; } = new List<TodosTags>();
    }
}
