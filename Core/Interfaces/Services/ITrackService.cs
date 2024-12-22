using Core.DTOs.Tracks;
using Core.Responses;

namespace Core.Interfaces.Services;

public interface ITrackService
{
    public Task<TrackResponse> GetTrack(string trackId);
    public Task<TracksResponse> GetTracks(QueryAllTracksDTO queryAllTracksDTO);
    public Task<TrackResponse> UploadTrack(UploadTrackDTO uploadTrackDTO, string userId);
    public Task RemoveTrack(string trackId);
    public Task UpdateTrackListening(Guid trackId);
}
