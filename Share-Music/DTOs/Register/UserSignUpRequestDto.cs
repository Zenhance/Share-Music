using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share_Music.DTOs.Register
{
    public class UserSignUpRequestDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
