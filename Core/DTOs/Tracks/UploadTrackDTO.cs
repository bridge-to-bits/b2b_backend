using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Tracks;

public class UploadTracksDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string PerformerId { get; set; }
    public IEnumerable<string> Genres { get; set; }
    public List<IFormFile>? MusicTracks { get; set; }
}
