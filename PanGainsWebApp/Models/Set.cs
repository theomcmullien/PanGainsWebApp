namespace PanGainsWebApp.Models
{
    public class Set
    {
        [Key]
        public int SetID { get; set; }
        [Required]
        public int YourExerciseID { get; set; } //relationship
        [Required]
        public int SetRow { get; set; }
        [Required]
        public string? SetType { get; set; }
        public string? Previous { get; set; }
        [Required]
        public int Kg { get; set; }
        [Required]
        public int Reps { get; set; }
    }
}
