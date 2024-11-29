namespace Users.Core.Responses;

public class ProfileResponse
{
    public string Banner { get; set; }
    public string Avatar { get; set; }
    public string Name { get; set; }
    public double Rating { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
    public string Location { get; set; }
    public string Desciption { get; set; }
}
