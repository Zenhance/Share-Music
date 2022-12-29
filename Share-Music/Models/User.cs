using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Share_Music.Models
{
    public class User:IdentityUser<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id {
            get { return base.Id; }
            set { base.Id = value; } 
        }
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
        public Role Role { get; set; } = Role.User;
    }
}
