using Core.DTOs.Tracks;
using Core.Responses.Tracks;

namespace Core.Interfaces.Services;

public interface ITrackService
{
    public Task<TrackResponse> GetTrack(string trackId);
    public Task<TracksResponse> GetTracks(QueryAllTracksDTO queryAllTracksDTO);
    public Task<TrackResponse> UploadTrack(UploadTrackDTO uploadTrackDTO, string userId);
    public Task RemoveTrack(string trackId);
    public Task UpdateTrackListening(Guid trackId);


    // ------------------  FAVORITE TRACKS ENDPOINTS SECTION   ----------------------------

    public Task<IEnumerable<FavoriteTrackResponse>> GetFavoriteTracks(Guid userId);
    public Task AddFavoriteTrack(Guid userId, Guid trackId);
    public Task RemoveFavoriteTrack(Guid userId, Guid trackId);
    public Task<bool> IsFavoriteTrack(Guid userId, Guid trackId);
}
