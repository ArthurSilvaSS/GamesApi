using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs.Authentication
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}
