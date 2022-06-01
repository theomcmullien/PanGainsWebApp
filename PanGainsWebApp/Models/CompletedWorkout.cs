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
        public DateTime DateCompleted { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public double TotalWeightLifted { get; set; }
    }
}
