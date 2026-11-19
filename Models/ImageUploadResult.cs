namespace FormsApp.Models
{
    public class ImageUploadResult
    {
        public bool IsSuccess { get; set; }
        public string? FileName { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
