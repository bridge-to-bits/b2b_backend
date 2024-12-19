namespace Core.Responses;

public class ProducerRelatedPerformerResponse
{
    public string Username { get; set; }
    public double Rating { get; set; }
    public string BackgroundPhoto { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
}
