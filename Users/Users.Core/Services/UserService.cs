using Users.Core.DTOs;
using Users.Core.Includes;
using Users.Core.Interfaces;
using Users.Core.Mapping;
using Users.Core.Models;
using Users.Core.Responses;

namespace Users.Core.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IAuthService authService
    ) : IUserService
{

    public async Task<User> Register(RegistrationDTO registrationDTO)
    {
        Dictionary<string, Func<string, Task>> _roleActions = new()
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

        var securityRole = registrationDTO.IsAdmin ? "Admin" : "User";
        await authService.CreateUserRole(user.Id.ToString(), securityRole);
        await authService.SetPermissions(
            user.Id.ToString(), 
            _rolePermissions.GetValueOrDefault(securityRole)!
        );

        if (_roleActions.TryGetValue(registrationDTO.Role.ToString(), out var attachRoleMethod))
        {
            await attachRoleMethod(user.Id.ToString());
        }

        return user;
    }

    public async Task<UserInfoResponse> GetUser(string id)
    {
        var user = await userRepository.GetUser(
            user => user.Id.ToString() == id, UserIncludes.ProducerAndPerformer)
            ?? throw new Exception("User do not exist");

        var rating = await GetUserAverageRating(id);

        var response = user.ToUserInfoResponse();
        response.UserType = DetermineUserType(user);
        response.Rating = rating;
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

    private static UserType DetermineUserType(User user)
    {
        if (user.Performer != null) return UserType.Performer;

        if (user.Producer != null) return UserType.Producer;

        throw new Exception("User type not found");
    }
}
