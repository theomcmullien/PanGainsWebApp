namespace PanGainsWebApp.Models
{
    public class ChallengeStats
    {
        [Key]
        public int ChallengeStatsID { get; set; }
        [Required]
        public int AccountID { get; set; } //relationship
        [Required]
        public int LeaderboardID { get; set; } //relationship
        [Required]
        public int ChallengeTotalReps { get; set; }
    }
}
