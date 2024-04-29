using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class UserDTO
    { 
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class UserNameDTO
    {
        [Required(ErrorMessage = "Name é obrigatório")]
        [MaxLength(50, ErrorMessage = "Name deve ter até 50 caracteres")]
        [MinLength(5, ErrorMessage = "Name deve ter no mínimo 5 caracteres")]
        public string Name { get; set; }
    }

    public class UserEmailDTO
    {
        [EmailAddress(ErrorMessage = "NewEmail informado incorreto")]
        [MaxLength(60, ErrorMessage = "NewEmail deve ter no máximo 60 caracteres")]
        public string Email { get; set; }
    }

    public class UserPasswordDTO
    {
        [Required(ErrorMessage = "Password é obrigatório")]
        [MaxLength(40, ErrorMessage = "Password deve ter até 40 caracteres")]
        [MinLength(5, ErrorMessage = "Password deve ter no mínimo 5 caracteres")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "OldPassword é obrigatório")]
        public string OldPassword { get; set; }
    }

    public class UserPasswordRecoveryDTO
    {
        [Required(ErrorMessage = "Password é obrigatório")]
        [MaxLength(40, ErrorMessage = "Password deve ter até 40 caracteres")]
        [MinLength(5, ErrorMessage = "Password deve ter no mínimo 5 caracteres")]
        public string Password { get; set; }
    }
}
