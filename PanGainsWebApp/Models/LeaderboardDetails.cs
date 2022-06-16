namespace PanGainsWebApp.Models
{
    public class LeaderboardDetails
    {
        public List<LeaderboardPosition> LeaderboardPositions { get; set; }
        public int LeaderboardID { get; set; }
        public string CurrentChallengeName { get; set; }
        public string NextChallengeName { get; set; }
    }
}
