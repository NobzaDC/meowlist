using MeowCore.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeowCore.Models
{
    public class Todos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TodoStatus Status { get; set; } = TodoStatus.Pending;

        // Foreign Key
        [Required]
        public int ListId { get; set; }

        [ForeignKey(nameof(ListId))]
        public Lists List { get; set; } = null!;

        // Relaciones
        public ICollection<TodosTags> TodosTags { get; set; } = new List<TodosTags>();
    }
}
