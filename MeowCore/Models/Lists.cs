using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeowCore.Models
{
    public class Lists
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Lists User { get; set; } = null!;

        // Relaciones
        public ICollection<Todos> Todos { get; set; } = new List<Todos>();
    }
}
