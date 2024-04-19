using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class RefreshTokenDTO
    {
        [Required(ErrorMessage = "O campo RefresToken é obrigatório")]
        public string RefreshToken { get; set; }
    }
}

