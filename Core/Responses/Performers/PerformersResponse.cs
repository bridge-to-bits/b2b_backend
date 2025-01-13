namespace Core.Responses.Performers;

public class PerformersResponse: PaginationResponse
{
    public IEnumerable<PerformerResponse> Data { get; set; }
}

public class PerformerResponse
{
    public string Avatar { get; set; }
    public string Username { get; set; }
    public IEnumerable<GenreResponse>? Genres { get; set; }
    public IEnumerable<SocialResponse>? Socials { get; set; }
    public double Rating { get; set; }
    public string Id { get; set; }
    public TrackShortResponse Track { get; set; }
}

public class TrackShortResponse
{
    public string Id { get; set; }
    public string Url { get; set; }
}