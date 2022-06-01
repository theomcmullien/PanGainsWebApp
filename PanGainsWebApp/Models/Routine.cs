namespace PanGainsWebApp.Models
{
    public class Routine
    {
        [Key]
        public int RoutineID { get; set; }
        [Required]
        public int FolderID { get; set; } //relationship
        [Required]
        public string? RoutineName { get; set; }
    }
}
