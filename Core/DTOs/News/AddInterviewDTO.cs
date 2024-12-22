namespace Core.DTOs.News;

public class AddInterviewDTO
{
    public Guid SenderId { get; set; }
    public Guid RespondentId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ContentPreview { get; set; }
    public string BackgroundPhotoUrl { get; set; }
    public string VideoLink { get; set; }
}
