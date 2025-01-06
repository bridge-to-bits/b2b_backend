namespace Core.Responses.Tracks;

public class TracksResponse : PaginationResponse
{
    public IEnumerable<TrackResponse> Data { get; set; }
}
