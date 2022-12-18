using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Share_Music.Models
{
    public class User : DbEntity
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }
}
