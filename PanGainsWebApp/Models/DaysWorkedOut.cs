namespace PanGainsWebApp.Models
{
    public class DaysWorkedOut
    {
        [Key]
        public int DaysWorkedOutID { get; set; }
        [Required]
        public int AccountID { get; set; } //relationship
        [Required]
        public string? Day { get; set; }
        [Required]
        public int Hours { get; set; }
    }
}
