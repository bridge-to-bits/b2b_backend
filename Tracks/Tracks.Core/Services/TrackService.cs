using System.Linq.Expressions;
using Tracks.Core.DTOs;
using Tracks.Core.Interfaces;
using Tracks.Core.Mapping;
using Tracks.Core.Models;
using Tracks.Core.Responses;

namespace Tracks.Core.Services;

public class TrackService(ITrackRepository trackRepository): ITrackService
{
    public async Task<TrackResponse> UploadTrack(UploadTrackDTO uploadTrackDTO)
    {
        var trackGenreIds = uploadTrackDTO.Genres;
        var genres = await trackRepository.GetGenres(trackGenreIds);

        var trackToCreate = uploadTrackDTO.ToTrack();
        trackToCreate.Genres = genres;

        var result = await trackRepository.CreateTrack(trackToCreate);
        var mappedResult = result.ToTrackResponse();
        mappedResult.Genres = genres;   
        return mappedResult;
    }

    public async Task<TrackResponse> GetTrack(string trackId)
    {
        var track = await trackRepository.GetTrack(
            track => track.Id == Guid.Parse(trackId), track => track.Genres);
        return track.ToTrackResponse();
    }

    public async Task<TracksResponse> GetTracks(QueryAllTracksDTO queryAllTracksDTO)
    {
        Expression<Func<Track, bool>> predicate = track =>
            (string.IsNullOrEmpty(queryAllTracksDTO.PerformerId)
                || track.PerformerId == Guid.Parse(queryAllTracksDTO.PerformerId))
            &&
            (queryAllTracksDTO.GenreIds == null
                || track.Genres.Any(genre => queryAllTracksDTO.GenreIds.Contains(genre.Id.ToString())));

        var tracks = await trackRepository.GetPaginationTracks(
            predicate,
            [track => track.Genres],
            queryAllTracksDTO.SortBy,
            queryAllTracksDTO.SortDirection?.ToLower() == "desc");

        int totalRecords = tracks.Count;
        int totalPages = (int)Math.Ceiling((double)totalRecords / queryAllTracksDTO.PageSize);

        tracks = tracks
            .Skip(queryAllTracksDTO.Skip)
            .Take(queryAllTracksDTO.PageSize)
            .ToList();

        var trackResponses = tracks.Select(DomainToDtoMapper.ToTrackResponse).ToList();

        var response = new TracksResponse
        {
            TotalRecords = totalRecords,
            CurrentPage = queryAllTracksDTO.PageNumber,
            TotalPages = totalPages,
            NextPage = queryAllTracksDTO.PageNumber < totalPages ? queryAllTracksDTO.PageNumber + 1 : null,
            PrevPage = queryAllTracksDTO.PageNumber > 1 ? queryAllTracksDTO.PageNumber - 1 : null,
            Data = trackResponses
        };

        return response;
    }

    public async Task RemoveTrack(string trackId)
    {
        var track = await trackRepository.GetTrack(track => track.Id == Guid.Parse(trackId));
        if (track != null)
        {
            await trackRepository.RemoveTrack(track);
        }
    }
}
