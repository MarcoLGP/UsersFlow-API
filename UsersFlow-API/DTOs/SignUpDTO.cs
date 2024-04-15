using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Name é obrigatório")]
        [MaxLength(50, ErrorMessage = "Name deve ter até 50 caracteres")]
        [MinLength(5, ErrorMessage = "Name deve ter no mínimo 5 caracteres")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Email informado incorreto")]
        [MaxLength(60, ErrorMessage = "Email deve ter no máximo 60 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password é obrigatório")]
        [MaxLength(40, ErrorMessage = "Password deve ter até 40 caracteres")]
        [MinLength(5, ErrorMessage = "Password deve ter no mínimo 5 caracteres")]
        public string Password { get; set; }
    }
}
