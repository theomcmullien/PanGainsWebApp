namespace PanGainsWebApp.Models
{
    public class Social
    {
        [Key]
        public int SocialID { get; set; }
        [Required]
        public int AccountID { get; set; } //relationship
        [Required]
        public int FollowingID { get; set; }
    }
}
