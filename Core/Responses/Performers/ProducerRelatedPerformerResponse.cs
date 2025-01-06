namespace Core.Responses.Performers;

public class ProducerRelatedPerformerResponse
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public double Rating { get; set; }
    public string BackgroundPhoto { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
}
