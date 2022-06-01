namespace PanGainsWebApp.Models
{
    public class YourExercise
    {
        [Key]
        public int YourExerciseID { get; set; }
        [Required]
        public int RoutineID { get; set; } //relationship
        [Required]
        public int ExerciseID { get; set; } //relationship
    }
}
