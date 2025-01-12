using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Core.Mapping;
using Core.Models;
using Core.DTOs.Tracks;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Auth;
using Core.Responses.Tracks;

namespace Core.Services;

public class TrackService(
    ITrackRepository trackRepository,
    IGenreRepository genreRepository,
    IPerformerService performerService,
    IUserRepository userRepository,
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
        var peformer = await performerService.GetPerformer(performer =>
            performer.UserId == Guid.Parse(queryAllTracksDTO.PerformerUserId));

        Expression<Func<Track, bool>> predicate = track =>
            (string.IsNullOrEmpty(queryAllTracksDTO.PerformerUserId)
                || track.PerformerId == peformer.Id)
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


    // ------------------  FAVORITE TRACKS ENDPOINTS SECTION   ----------------------------


    public async Task<IEnumerable<FavoriteTrackResponse>> GetFavoriteTracks(Guid userId)
    {
        var favoriteTracks = await trackRepository.GetFavoriteTracks(userId);
        var favTrackIds = favoriteTracks.Select(f => f.TrackId).ToList();
        var tracks = await trackRepository.GetTracks(track => favTrackIds.Contains(track.Id));

        return tracks?.ToFavoriteTracksResponse();
    }

    public async Task AddFavoriteTrack(Guid userId, Guid trackId)
    {
        var userExist = await userRepository.Exist(u => u.Id == userId);
        if (!userExist) throw new Exception("User not found");

        var trackExist = await trackRepository.Exist(trackId);
        if (!trackExist) throw new Exception("Track not found");

        var favoriteTrack = new FavoriteTrack
        {
            UserId = userId,
            TrackId = trackId,
        };

        await trackRepository.AddFavoriteTrack(favoriteTrack);
    }

    public async Task RemoveFavoriteTrack(Guid userId, Guid trackId)
    {
        var userExist = await userRepository.Exist(u => u.Id == userId);
        if (!userExist) throw new Exception("User not found");

        var trackExist = await trackRepository.Exist(trackId);
        if (!trackExist) throw new Exception("Track not found");

        var favoriteTrack = new FavoriteTrack
        {
            UserId = userId,
            TrackId = trackId,
        };

        await trackRepository.RemoveFavoriteTrack(favoriteTrack);
    }

    public Task<bool> IsFavoriteTrack(Guid userId, Guid trackId)
    {
        return trackRepository.IsFavoriteTrack(userId, trackId);
    }
}
