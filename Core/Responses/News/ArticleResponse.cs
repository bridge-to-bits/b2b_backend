namespace Core.Responses.News;

public class ArticleResponse
{
    public Guid Id { get; set; }
    public string ContentPreview { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string BackgroundPhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public NewsAuthorResponse Author { get; set; }
    public IEnumerable<NewsCommentResponse> Comments { get; set; }
    public double Rating { get; set; }
}
