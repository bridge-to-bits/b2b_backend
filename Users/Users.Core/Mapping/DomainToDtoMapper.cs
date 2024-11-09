using Users.Core.Models;
using Users.Core.Responses;

namespace Users.Core.Mapping;
public static class DomainToDtoMapper
{
    public static UserInfoResponse ToUserInfoResponse(this User user)
    {
        return new UserInfoResponse
        {
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            City = user.City,
            Avatar = user.Avatar,
            AboutMe = user.AboutMe,
        };
    }
}
