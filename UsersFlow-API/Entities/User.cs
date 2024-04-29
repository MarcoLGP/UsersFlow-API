using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(60)]
        public string Email { get; set; }
        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        public ICollection<Note>? Notes { get; set; }
    }
}
