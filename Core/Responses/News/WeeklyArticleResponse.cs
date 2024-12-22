namespace Core.Responses.News;

public class WeeklyArticleResponse
{
    public Guid Id { get; set; }
    public string ContentPreview { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string BackgroundPhotoUrl { get; set; }
}

public class WeeklyArticlesResponse
{
    public IEnumerable<WeeklyArticleResponse> Data { get; set; }
}