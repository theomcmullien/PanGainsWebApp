namespace PanGainsWebApp.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseID { get; set; }
        [Required]
        public string? ExerciseName { get; set; }
    }
}
