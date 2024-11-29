using System.Linq.Expressions;
using Users.Core.DTOs;
using Users.Core.Models;
using Users.Core.Responses;

namespace Users.Core.Interfaces;

public interface IUserService
{
    public Task<User> Register(MainRegistrationDTO user);
    public Task<string> Login(LoginDTO user);

    public Task<IEnumerable<GenreResponse>> GetAvailableGenres();
    public Task<GenreResponse> AddAvailableGenre(AddGenreDTO addGenreDTO);

    public Task<UserInfoResponse> GetUser(string id);
    public Task<UsersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO);
    public Task<UsersResponse> GetProducers(QueryAllUsersDTO queryAllUsersDTO);

    public Task AddRating(AddRatingDTO addRatingDTO, string targetUserId);

    public Task<ProfileResponse> GetUserProfile(string userId);
    public Task<ProfileResponse> UpdateUserProfile(string userId, UpdateProfileDTO updateProfileDTO);

    public Task<User?> GetMe (string userId);
}
