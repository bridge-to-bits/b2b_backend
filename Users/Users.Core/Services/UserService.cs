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

    public async Task<User> Register(RegistrationDTO registrationDTO)
    {
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
        if (registrationDTO.Socials != null)
        {
            foreach (var social in registrationDTO.Socials)
            {
                await socialRepository.AddSocial(DtoToDomainMapper.ToSocial(social));
            }
        }
        var userGenres = await genreRepository.GetGenres(
            genre => registrationDTO.GenreIds.Contains(genre.Id.ToString()));

        var user = registrationDTO.ToUser();
        user.Genres = userGenres;
        user.Password = hashPasword;
        await userRepository.CreateUser(user);

        var securityRole = registrationDTO.IsAdmin ? "Admin" : "User";
        await authService.CreateUserRole(user.Id.ToString(), securityRole);
        await authService.SetPermissions(
            user.Id.ToString(),
            _rolePermissions.GetValueOrDefault(securityRole)!
        );

        if (_typeActions.TryGetValue(registrationDTO.Type.ToString(), out var attachTypeMethod))
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

    public async Task<UsersResponse> GetUsers(QueryAllUsersDTO queryAllUsersDTO)
    {
        Expression<Func<User, bool>> predicate = user =>
            (queryAllUsersDTO.GenreIds == null || 
                user.Genres.Any(genre => queryAllUsersDTO.GenreIds.Contains(genre.Id.ToString())))
            &&
            (queryAllUsersDTO.UserType == user.UserType);

        var users = await userRepository.GetPaginationUsers(
            predicate,
            UserIncludes.AllRelations,
            queryAllUsersDTO.SortBy,
            queryAllUsersDTO.SortDirection?.ToLower() == "desc",
            queryAllUsersDTO.Skip,
            queryAllUsersDTO.PageSize);

        int totalRecords = await userRepository.Count(predicate);
        int totalPages = (int)Math.Ceiling((double)totalRecords / queryAllUsersDTO.PageSize);

        var userResponses = users.Select(DomainToDtoMapper.ToUserInfoResponse).ToList();

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
        return genres.Select(DomainToDtoMapper.ToGenreResponse).ToList();
    }
}
