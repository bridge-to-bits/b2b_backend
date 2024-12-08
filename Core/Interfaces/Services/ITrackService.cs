using Core.DTOs.Tracks;
using Core.Responses;

namespace Core.Interfaces.Services;

public interface ITrackService
{
    public Task<TrackResponse> GetTrack(string trackId);
    public Task<TracksResponse> GetTracks(QueryAllTracksDTO queryAllTracksDTO);
    public Task<List<TrackResponse>> UploadTracks(UploadTracksDTO uploadTrackDTO);
    public Task RemoveTrack(string trackId);
}
