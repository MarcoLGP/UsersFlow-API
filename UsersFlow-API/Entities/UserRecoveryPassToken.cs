using System.ComponentModel.DataAnnotations;
using UsersFlow_API.Models;

namespace UsersFlow_API.Entities
{
    public class UserRecoveryPassToken
    {

        [Key]
        public int RecoveryPassTokenId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string RecoveryPassToken { get; set; }
    }
}
