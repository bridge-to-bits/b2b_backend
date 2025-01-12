using Common.Models;

namespace Core.DTOs.Tracks;

public class QueryAllTracksDTO : QueryAllDTO
{
    public string? PerformerUserId { get; set; }
    public List<string>? GenreIds { get; set; }
}
