namespace PanGainsWebApp.Models
{
    public class CreateLeaderboard
    {
        [Key]
        public int LeaderboardID { get; set; }
        public DateTime LeaderboardDate { get; set; } = DateTime.Now.AddMonths(1);
        public int TotalParticipants { get; set; } = 0;
        public List<Challenges> Challenges { get; set; }

        public int ChallengesID { get; set; }
    }
}
