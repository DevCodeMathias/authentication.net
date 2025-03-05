using System.ComponentModel.DataAnnotations;

namespace authentication_API.domain.dto
{
    public class UserDto
    {
        [Required(ErrorMessage = " email é o obrigatorio")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "senha obrigatorio")]
        public string PasswordHash { get; set; }
    }
}
