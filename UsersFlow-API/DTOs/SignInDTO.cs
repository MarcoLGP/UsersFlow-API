using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class SignInRequestDTO
    {
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(60)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password é obrigatório")]
        [MaxLength(40, ErrorMessage = "Password deve ter até 40 caracteres")]
        [MinLength(5, ErrorMessage = "Password deve ter no mínimo 5 caracteres")]
        public string Password { get; set; }
    }

    public class SignInResponseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
