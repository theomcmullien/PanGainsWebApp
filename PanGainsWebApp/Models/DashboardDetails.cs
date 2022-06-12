namespace PanGainsWebApp.Models
{
    public class DashboardDetails
    {
        public int AccountsCount { get; set; }
        public int PremiumAccountsCount { get; set; }
        public int ExercisesCount { get; set; }
        public int ChallengesCount { get; set; }
        public List<Account> RecentAccounts { get; set; }
        public List<LeaderboardPosition> LeaderboardPositions { get; set; }
    }
}
