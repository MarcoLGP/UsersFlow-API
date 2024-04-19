using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersFlow_API.Models
{
    public class User
    {
        public User()
        {
            Notes = new Collection<Note>();
        }        
        [Key]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        public ICollection<Note>? Notes { get; set; }
    }
}
