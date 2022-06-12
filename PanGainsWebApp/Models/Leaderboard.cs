namespace PanGainsWebApp.Models
{
    public class Leaderboard
    {
        [Key]
        public int LeaderboardID { get; set; }
        public int ChallengesID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LeaderboardDate { get; set; }
        [Required]
        public int TotalParticipants { get; set; }
    }
}
