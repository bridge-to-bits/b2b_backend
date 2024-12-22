using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Tracks;

public class UploadTrackDTO
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<string> GenreIds { get; set; }
    public IFormFile Track { get; set; }
}
