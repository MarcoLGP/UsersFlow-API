using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo UrlBase é obrigatório")]
        public string UrlBase { get; set; }
    }
}
