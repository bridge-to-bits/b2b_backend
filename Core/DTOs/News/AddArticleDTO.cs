namespace Core.DTOs.News;

public class AddArticleDTO
{
    public Guid SenderId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ContentPreview { get; set; }
    public string BackgroundPhotoUrl { get; set; }
}