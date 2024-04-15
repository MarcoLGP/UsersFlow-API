using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersFlow_API.Models
{
    public class UserRefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
