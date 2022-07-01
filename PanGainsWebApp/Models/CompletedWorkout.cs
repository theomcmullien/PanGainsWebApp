namespace PanGainsWebApp.Models
{
    public class CompletedWorkout
    {
        [Key]
        public int CompletedWorkoutID { get; set; }
        [Required]
        public int AccountID { get; set; }
        [Required]
        public int RoutineID { get; set; }
        [Required]
        public string? DateCompleted { get; set; }
        [Required]
        public string? Duration { get; set; }
        [Required]
        public double TotalWeightLifted { get; set; }
    }
}
