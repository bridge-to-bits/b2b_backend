namespace Core.Responses;

public class FavoritePerformerResponse
{
    public string Avatar {  get; set; }
    public string Username { get; set; }
    public IEnumerable<SocialResponse> Socials { get; set; }
    public double Rating { get; set; }
    public string UserId { get; set; }
}
