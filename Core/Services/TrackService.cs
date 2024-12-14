using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Core.Mapping;
using Core.Models;
using Core.Responses;
using Core.DTOs.Tracks;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Auth;

namespace Core.Services;

public class TrackService(
    ITrackRepository trackRepository,
    IOptions<GoogleDriveOptions> googleDriveOptions) : ITrackService
{
    private readonly FileService fileService = new(
        googleDriveOptions.Value.ServiceFilePath,
        googleDriveOptions.Value.FolderId
    );

    public async Task<List<TrackResponse>> UploadTracks(UploadTracksDTO uploadTrackDTO)
    {
        var trackGenreIds = uploadTrackDTO.Genres;
        var genres = await trackRepository.GetGenres(trackGenreIds);
        var createdTracks = new List<Track>();

        if (uploadTrackDTO.MusicTracks == null || uploadTrackDTO.MusicTracks.Count == 0)
        {
            return null;
        }

        foreach (var track in uploadTrackDTO.MusicTracks)
        {
            var originalFileName = Path.GetFileNameWithoutExtension(track.FileName);

            var musicTrackUrl = await fileService.UploadFileAsync(
                track,
                $"{uploadTrackDTO.PerformerId}_{originalFileName}"
            );

            var trackToCreate = uploadTrackDTO.ToTrack();
            trackToCreate.Url = musicTrackUrl;
            trackToCreate.Genres = genres;

            var result = await trackRepository.CreateTrack(trackToCreate);
            createdTracks.Add(result);
        }

        var mappedResult = createdTracks.ToTracksResponse();
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

        var trackResponses = tracks.Select(DomainToResponseMapper.ToTrackResponse).ToList();

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
