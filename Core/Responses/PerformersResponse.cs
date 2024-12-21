namespace Core.Responses;

public class PerformersResponse
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
    public string UserId { get; set; }
}
