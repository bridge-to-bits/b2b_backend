using Core.Models;

namespace Core.Responses;

public class TrackResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
    public Guid PerformerId { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
}
