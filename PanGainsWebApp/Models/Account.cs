global using System.ComponentModel.DataAnnotations;

namespace PanGainsWebApp.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        [Required]
        public string? Firstname { get; set; }
        [Required]
        public string? Lastname { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public byte[]? PasswordHash { get; set; }
        [Required]
        public byte[]? PasswordSalt { get; set; }
        public string? Title { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool Private { get; set; }
        [Required]
        public bool Notifications { get; set; }
        [Required]
        public int AverageChallengePos { get; set; }
        [Required]
        public string? Type { get; set; }
        public string? Role { get; set; }

    }
}
