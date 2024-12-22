namespace Core.Responses.News;

public class NewsCommentResponse
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public NewsComentatorResponse Comentator { get; set; }
}
