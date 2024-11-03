using Common.Models;

namespace Tracks.Core.DTOs
{
    public class QueryAllTracksDTO : QueryAllDTO
    {
        public string? PerformerId { get; set; }
        public List<string>? GenreIds { get; set; }
    }
}
