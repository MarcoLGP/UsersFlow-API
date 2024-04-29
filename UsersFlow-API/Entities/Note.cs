using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersFlow_API.Utils;

namespace UsersFlow_API.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(400)]
        public string Content { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        [Required]
        public bool Public { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
