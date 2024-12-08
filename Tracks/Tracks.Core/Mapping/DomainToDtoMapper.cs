using Tracks.Core.Models;
using Tracks.Core.Responses;

namespace Tracks.Core.Mapping;

public static class DomainToDtoMapper
{
    public static GenreResponse ToGenreResponse(this Genre genre)
    {
        return new GenreResponse
        {
            Id = genre.Id,
            Name = genre.Name,
        };
    }

    public static TrackResponse ToTrackResponse(this Track track)
    {
        return new TrackResponse
        {
            Id = track.Id,
            Name = track.Name,
            Description = track.Description,
            Genres = track.Genres,
            PerformerId = track.PerformerId,
            Url = track.Url,
        };
    }

    public static List<TrackResponse> ToTracksResponse(this List<Track> tracks)
    {
        return tracks.Select(ToTrackResponse).ToList();
    }
}
