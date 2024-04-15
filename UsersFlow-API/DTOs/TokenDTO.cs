using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class TokenDTO
    {
        [Required(ErrorMessage = "O Campo Token é obrigatório")]
        public string Token { get; set; }
        [Required(ErrorMessage = "O campo RefresToken é obrigatório")]
        public string RefreshToken { get; set; }
    }
}
