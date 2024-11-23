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
            Socials = user.Socials?.Select(ToSocialResponse).ToList(),
            Genres = user.Genres?.Select(ToGenreResponse).ToList(),
        };
    }

    public static SocialResponse ToSocialResponse(this Social social) 
    {
        return new SocialResponse
        {
            Name = social.Name,
            Link = social.Link,
        };
    }

    public static GenreResponse ToGenreResponse(this Genre genre)
    {
        return new GenreResponse
        {
            Id = genre.Id.ToString(),
            Name = genre.Name,
        };
    }
}
