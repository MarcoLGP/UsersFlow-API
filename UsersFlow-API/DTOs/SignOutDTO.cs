using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class SignOutDTO
    {
        [Required(ErrorMessage = "O campo RefreshToken é obrigatório")]
        public string RefreshToken { get; set; }
    }
}
