using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.Models
{
    public class UserRefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
