namespace PanGainsWebApp.Models
{
    public class Statistics
    {
        [Key]
        public int StatisticsID { get; set; }
        [Required]
        public int AccountID { get; set; } //relationship
        [Required]
        public int TotalWorkouts { get; set; }
        [Required]
        public int AvgWorkoutTime { get; set; }
        [Required]
        public double TotalLifted { get; set; }
        [Required]
        public int AvgReps { get; set; }
        [Required]
        public int AvgSets { get; set; }
    }
}
