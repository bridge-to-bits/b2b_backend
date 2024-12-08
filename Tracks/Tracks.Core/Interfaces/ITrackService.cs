using Tracks.Core.DTOs;
using Tracks.Core.Responses;

namespace Tracks.Core.Interfaces;

public interface ITrackService
{
    public Task<TrackResponse> GetTrack(string trackId);
    public Task<TracksResponse> GetTracks(QueryAllTracksDTO queryAllTracksDTO);
    public Task<List<TrackResponse>> UploadTracks(UploadTracksDTO uploadTrackDTO);
    public Task RemoveTrack(string trackId);
}
