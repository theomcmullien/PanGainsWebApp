namespace PanGainsWebApp.Models
{
    public class RoutineWithExercises
    {
        public int RoutineID { get; set; }
        public string? RoutineName { get; set; }
        public List<string>? Exercises { get; set; }
    }
}
