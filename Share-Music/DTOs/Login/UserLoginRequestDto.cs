using System.ComponentModel.DataAnnotations;

namespace Share_Music.DTOs.Login
{
    public class UserLoginRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
