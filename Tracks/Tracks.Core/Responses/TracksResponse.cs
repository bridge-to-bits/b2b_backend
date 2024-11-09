using Common.Models;

namespace Tracks.Core.Responses;

public class TracksResponse: PaginationResponse
{
    public IEnumerable<TrackResponse> Data { get; set; }
}
