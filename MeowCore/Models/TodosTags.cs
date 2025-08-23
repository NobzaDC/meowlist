using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeowCore.Models
{
    public class TodosTags
    {
        [Required]
        public int TodoId { get; set; }

        [Required]
        public int TagId { get; set; }

        [ForeignKey(nameof(TodoId))]
        public Todos Todo { get; set; } = null!;

        [ForeignKey(nameof(TagId))]
        public Tags Tag { get; set; } = null!;
    }
}
