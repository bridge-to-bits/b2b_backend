namespace Core.Responses.News;

public class WeeklyInterviewResponse
{
    public Guid Id { get; set; }
    public string ContentPreview { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string BackgroundPhotoUrl { get; set; }
    public string VideoLink { get; set; }
}

public class WeeklyInterviewsResponse
{
    public IEnumerable<WeeklyInterviewResponse> Data { get; set; }
}
