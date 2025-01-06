using Core.Models;
using Core.Responses;
using Core.DTOs.Users;
using Core.Responses.Users;
using Core.Responses.Tracks;
using Core.Responses.Performers;

namespace Core.Interfaces.Services;

public interface IUserService
{
    public Task<User> Register(MainRegistrationDTO user);
    public Task<string> Login(LoginDTO user);

    public Task<IEnumerable<GenreResponse>> GetAvailableGenres();
    public Task<GenreResponse> AddAvailableGenre(AddGenreDTO addGenreDTO);

    public Task<UserInfoResponse> GetUser(string id);
    public Task<UsersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO);
    public Task<UsersResponse> GetProducers(QueryAllUsersDTO queryAllUsersDTO);

    public Task AddRating(AddRatingDTO addRatingDTO, string targetUserId, string initiatorUserId);
    public Task<double> GetUserAverageRating(string userId);
    public Task<UserRatingInfoResponse> GetGivenUserRateInfo(string targetUserId, string initiatorUserId);

    public Task<ProfileResponse> GetUserProfile(string userId);
    public Task<ProfileResponse> UpdateUserProfile(string userId, UpdateProfileDTO updateProfileDTO);

    public Task<User?> GetMe(string userId);
    public Task<UserType> GetUserType(Guid userId);


    // ------------------  FAVORITE PERFORMERS ENDPOINTS SECTION   ----------------------------
    public Task<IEnumerable<FavoritePerformerResponse>> GetFavoritePerformers(Guid userId);
    public Task AddFavoritePerformer(Guid userId, Guid performerId); 
    public Task RemoveFavoritePerformer(Guid userId, Guid performerId);


    // ------------------  FAVORITE TRACKS ENDPOINTS SECTION   ----------------------------
    public Task<IEnumerable<FavoriteTrackResponse>> GetFavoriteTracks(Guid userId);
    public Task AddFavoriteTrack(Guid userId, Guid trackId);
    public Task RemoveFavoriteTrack(Guid userId, Guid trackId);
}
