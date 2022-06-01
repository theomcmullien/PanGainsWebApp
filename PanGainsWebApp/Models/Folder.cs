namespace PanGainsWebApp.Models
{
    public class Folder
    {
        [Key]
        public int FolderID { get; set; }
        [Required]
        public int AccountID { get; set; } //relationship
        [Required]
        public string? FolderName { get; set; }
        [Required]
        public int FolderLikes { get; set; }
    }
}
