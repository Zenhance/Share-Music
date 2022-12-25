using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Share_Music.Models
{
    public class User:IdentityUser,DbEntity
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public override string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public override  string Email { get; set; } = string.Empty;
        [Required]
        public new byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public bool IsVerified { get; set; } = false;
        public Role Role { get; set; } = Role.User;
    }
}
