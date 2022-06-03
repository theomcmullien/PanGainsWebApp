namespace PanGainsWebApp.Models
{
    public class AdminAccount
    {
        [Key]
        public int AdminAccountID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
