namespace PanGainsWebApp.Models
{
    public class UserAuth
    {
        [Key]
        public int UserAuthID { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PasswordSalt { get; set; }
        [Required]
        public string? PasswordHash { get; set; }

    }
}
