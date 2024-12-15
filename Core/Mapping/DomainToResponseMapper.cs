using Core.Models;
using Core.Responses;

namespace Core.Mapping;
public static class DomainToResponseMapper
{
    public static UserInfoResponse ToUserInfoResponse(this User user)
    {
        return new UserInfoResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            Username = user.Username,
            LastName = user.LastName,
            City = user.City,
            Avatar = user.Avatar,
            ProfileBackground = user.ProfileBackground,
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

    public static ProfileResponse ToProfileResponse(this User user, double rating)
    {
        return new ProfileResponse
        {
            Banner = "/banner.png",
            Avatar = user.Avatar,
            Username = user.Username,
            Description = user.AboutMe,
            Location = user.City,
            Rating = rating,
            UserType = user.UserType,
            Genres = user.Genres.Select(ToGenreResponse).ToList(),
            Socials = user.Socials.Select(ToSocialResponse).ToList(),
        };
    }

    public static GetMeResponse ToGetMeResponse(this User user) 
    {
        return new GetMeResponse
        {
            Id = user.Id.ToString(),
            Username = user.Username,
            Email = user.Email,
            Avatar = user.Avatar ?? "",
        };
    }

    public static TrackResponse ToTrackResponse(this Track track)
    {
        return new TrackResponse
        {
            Id = track.Id,
            Name = track.Name,
            Description = track.Description,
            Genres = track.Genres,
            PerformerId = track.PerformerId,
            Url = track.Url,
        };
    }

    public static List<TrackResponse> ToTracksResponse(this List<Track> tracks)
    {
        return tracks.Select(ToTrackResponse).ToList();
    }
}
