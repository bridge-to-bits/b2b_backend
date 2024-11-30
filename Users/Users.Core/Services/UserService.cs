using System.Linq.Expressions;
using Users.Core.DTOs;
using Users.Core.Includes;
using Users.Core.Interfaces;
using Users.Core.Mapping;
using Users.Core.Models;
using Users.Core.Responses;

namespace Users.Core.Services;

public class UserService(
    IUserRepository userRepository,
    ISocialRepository socialRepository,
    IGenreRepository genreRepository,
    IPasswordHasher passwordHasher,
    IAuthService authService
    ) : IUserService
{

    public async Task<User> Register(MainRegistrationDTO registrationDTO)
    {
        var existingUser = await userRepository.GetUser(user => user.Email == registrationDTO.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already in use");
        }

        Dictionary<string, Func<string, Task>> _typeActions = new()
        {
            { "Producer", AttachProducer },
            { "Performer", AttachPerformer }
        };

        Dictionary<string, IEnumerable<string>> _rolePermissions = new()
        {
            { "User", ["addRating"] },
            { "Admin", ["setPermissions"] }
        };

        string hashPasword = passwordHasher.HashPassword(registrationDTO.Password);

        var user = registrationDTO.ToUser();
        user.Password = hashPasword;
        await userRepository.CreateUser(user);

        var securityRole = "User";
        await authService.CreateUserRole(user.Id.ToString(), securityRole);
        await authService.SetPermissions(
            user.Id.ToString(),
            _rolePermissions.GetValueOrDefault(securityRole)!
        );

        if (_typeActions.TryGetValue(registrationDTO.UserType.ToString(), out var attachTypeMethod))
        {
            await attachTypeMethod(user.Id.ToString());
        }

        return user;
    }

    public async Task<UserInfoResponse> GetUser(string id)
    {
        var user = await userRepository.GetUser(
            user => user.Id.ToString() == id, UserIncludes.AllRelations)
            ?? throw new Exception("User do not exist");

        var rating = await GetUserAverageRating(id);

        var response = user.ToUserInfoResponse();
        response.Rating = rating;
        return response;
    }

    public async Task<UsersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO)
    {
        return await GetUsersByType(queryAllUsersDTO, UserType.Performer);
    }

    public async Task<UsersResponse> GetProducers(QueryAllUsersDTO queryAllUsersDTO)
    {
        return await GetUsersByType(queryAllUsersDTO, UserType.Producer);
    }

    private async Task<UsersResponse> GetUsersByType(QueryAllUsersDTO queryAllUsersDTO, UserType userType)
    {
        Expression<Func<User, bool>> predicate = user =>
            (queryAllUsersDTO.GenreIds == null ||
                user.Genres.Any(genre => queryAllUsersDTO.GenreIds.Contains(genre.Id.ToString())))
            &&
            (user.UserType == userType);

        var users = await userRepository.GetPaginationUsers(
            predicate,
            UserIncludes.AllRelations,
            queryAllUsersDTO.SortBy,
            queryAllUsersDTO.SortDirection?.ToLower() == "desc",
            queryAllUsersDTO.Skip,
            queryAllUsersDTO.PageSize);

        int totalRecords = await userRepository.Count(predicate);
        int totalPages = (int)Math.Ceiling((double)totalRecords / queryAllUsersDTO.PageSize);

        var userResponses = users.Select(DomainToResponseMapper.ToUserInfoResponse).ToList();

        var response = new UsersResponse
        {
            TotalRecords = totalRecords,
            CurrentPage = queryAllUsersDTO.PageNumber,
            TotalPages = totalPages,
            NextPage = queryAllUsersDTO.PageNumber < totalPages ? queryAllUsersDTO.PageNumber + 1 : null,
            PrevPage = queryAllUsersDTO.PageNumber > 1 ? queryAllUsersDTO.PageNumber - 1 : null,
            Data = userResponses
        };

        return response;
    }

    public async Task<string> Login(LoginDTO loginDTO)
    {
        var user = await userRepository.GetUser(user => user.Email == loginDTO.Email)
            ?? throw new Exception("User do not exist");

        var check = passwordHasher.VerifyHashedPassword(loginDTO.Password, user.Password);
        if (!check)
        {
            throw new Exception("wrong password");
        }

        var token = authService.GenerateToken(user.Id.ToString());
        return token;
    }

    public async Task<string> AttachProducer(string userId)
    {
        var producer = await userRepository.AttachEntityToUser<Producer>(userId);
        return producer.Id.ToString();
    }

    public async Task<string> AttachPerformer(string userId)
    {
        var performer = await userRepository.AttachEntityToUser<Performer>(userId);
        return performer.Id.ToString();
    }

    public async Task AddRating(AddRatingDTO addRatingDTO, string targetUserId)
    {
        await userRepository.AddRating(new Rating()
        {
            InitiatorUserId = Guid.Parse(addRatingDTO.InitiatorUserId),
            TargetUserId = Guid.Parse(targetUserId),
            RatingValue = addRatingDTO.Rating
        });
    }

    public async Task<double> GetUserAverageRating(string userId)
    {
        var ratings = await userRepository.GetRatingsForUser(userId);
        return ratings.Count != 0 ? ratings.Average(r => r.RatingValue) : 0;
    }

    public async Task<IEnumerable<GenreResponse>> GetAvailableGenres()
    {
        var genres = await genreRepository.GetGenres(genre => true);
        return genres.Select(DomainToResponseMapper.ToGenreResponse).ToList();
    }

    public async Task<GenreResponse> AddAvailableGenre(AddGenreDTO addGenreDTO)
    {
        var genre = addGenreDTO.ToGenre();
        var result = await genreRepository.AddGenre(genre);
        return result.ToGenreResponse();
    }

    public async Task<ProfileResponse> GetUserProfile(string userId)
    {
        var user = await userRepository.GetUser(user => user.Id == Guid.Parse(userId), UserIncludes.Genres);
        var rating = await GetUserAverageRating(userId);
        return user.ToProfileResponse(rating);
    }

    public async Task<ProfileResponse> UpdateUserProfile(string userId, UpdateProfileDTO updateProfileDTO)
    {
        var user = await userRepository.GetUserForUpdate(
            user => user.Id == Guid.Parse(userId), 
            [..UserIncludes.Genres, ..UserIncludes.Socials]
        ) ?? throw new NullReferenceException();

        user.UpdateUser(updateProfileDTO);

        if (updateProfileDTO.Socials != null)
        {
            UpdateUserSocials(user, updateProfileDTO.Socials);
        }

        if (updateProfileDTO.GenreIds != null)
        {
            await UpdateUserGenres(user, updateProfileDTO.GenreIds);
        }

        await userRepository.SaveAsync();

        var rating = await GetUserAverageRating(userId);

        return user.ToProfileResponse(rating);
    }

    private async Task UpdateUserGenres(User user, IEnumerable<string> genreIds)
    {
        if (user.Genres == null)
            user.Genres = new List<Genre>();

        var existingGenres = user.Genres;

        var genresToRemove = existingGenres
            .Where(existingGenre => !genreIds.Contains(existingGenre.Id.ToString()))
            .ToList();

        foreach (var genre in genresToRemove)
        {
            user.Genres.Remove(genre);
        }

        var existingGenreIds = existingGenres.Select(genre => genre.Id.ToString());
        var genreIdsToAdd = genreIds.Except(existingGenreIds).ToList();

        var genresToAdd = await genreRepository.GetGenres(genre => genreIdsToAdd.Contains(genre.Id.ToString()));

        foreach (var genre in genresToAdd)
        {
            user.Genres.Add(genre);
        }
    }

    private void UpdateUserSocials(User user, IEnumerable<AddSocialDTO> updatedSocials)
    {
        var updatedSocialsMap = updatedSocials.ToDictionary(s => s.Name, s => s.Link);

        var existingSocials = user.Socials ?? [];
        var socialsToRemove = existingSocials
            .Where(existingSocial => !updatedSocialsMap.ContainsKey(existingSocial.Name))
            .ToList();

        foreach (var social in socialsToRemove)
        {
            user.Socials?.Remove(social);
        }

        foreach (var updatedSocial in updatedSocials)
        {
            var existingSocial = existingSocials.FirstOrDefault(s => s.Name == updatedSocial.Name);
            if (existingSocial != null && existingSocial.Link != updatedSocial.Link)
            {
                existingSocial.Link = updatedSocial.Link;
            }
            else
            {
                user.Socials?.Add(new Social
                {
                    Id = Guid.NewGuid(),
                    Name = updatedSocial.Name,
                    Link = updatedSocial.Link,
                    UserId = user.Id
                });
            }
        }
    }
    public Task<User?> GetMe(string userId)
    {
        return userRepository.GetUserForUpdate(user => user.Id == Guid.Parse(userId));
    }
}
