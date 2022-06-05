namespace PanGainsWebApp.Models
{
    public class AccountDetails
    {
        public Account Account { get; set; }
        public Statistics Statistics { get; set; }
        public List<DaysWorkedOut> DaysWorkedOutList { get; set; }
        public int Following { get; set; }
        public int Followers { get; set; }
    }
}
