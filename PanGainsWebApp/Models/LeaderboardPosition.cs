namespace PanGainsWebApp.Models
{
    public class LeaderboardPosition : IComparable<LeaderboardPosition>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Amount { get; set; }

        public LeaderboardPosition(string firstname, string lastname, int amount)
        {
            Firstname = firstname;
            Lastname = lastname;
            Amount = amount;
        }

        public int CompareTo(LeaderboardPosition? other)
        {
            if (other == null) return 1;
            return Amount - other.Amount;
        }
    }
}
