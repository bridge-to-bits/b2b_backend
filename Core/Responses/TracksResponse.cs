namespace Core.Responses;

public class TracksResponse : PaginationResponse
{
    public IEnumerable<TrackResponse> Data { get; set; }
}
