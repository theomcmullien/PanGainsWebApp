namespace PanGainsWebApp.Models
{
    public class Challenges
    {
        [Key]
        public int ChallengesID { get; set; }
        [Required]
        public string? ChallengeName { get; set; }
    }
}
