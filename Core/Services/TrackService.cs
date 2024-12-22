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
    IGenreRepository genreRepository,
    IPerformerService performerService,
    IOptions<GoogleDriveOptions> googleDriveOptions) : ITrackService
{
    private readonly FileService fileService = new(
        googleDriveOptions.Value.ServiceFilePath,
        googleDriveOptions.Value.FolderId
    );

    public async Task<TrackResponse> UploadTrack(UploadTrackDTO uploadTrackDTO, string userId)
    {
        var performer = await performerService.GetPerformer(
            performer => performer.UserId == Guid.Parse(userId))
            ?? throw new Exception("Performer related to such user do not exist");

        if (uploadTrackDTO.Track == null)
        {
            return null;
        }

        var musicTrackUrl = await fileService.UploadFileAsync(
            uploadTrackDTO.Track,
            $"{performer.Id}_{Path.GetFileNameWithoutExtension(uploadTrackDTO.Track.FileName)}"
        );

        var genres = await genreRepository.GetGenres(genre => uploadTrackDTO.GenreIds.Contains(genre.Id.ToString()));

        var trackToCreate = new Track
        {
            Name = uploadTrackDTO.Name,
            Description = uploadTrackDTO.Description,
            Url = musicTrackUrl,
            PerformerId = performer.Id,
            Genres = genres
        };

        var result = await trackRepository.CreateTrack(trackToCreate);

        return result.ToTrackResponse();
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

    public Task UpdateTrackListening(Guid trackId)
    {
        return trackRepository.IncrementTrackListenings(trackId);
    }
}
