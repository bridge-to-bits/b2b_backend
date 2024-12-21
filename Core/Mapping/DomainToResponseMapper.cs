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

    public static FavoritePerformerResponse ToFavoritePerformerResponse(this User user)
    {
        return new FavoritePerformerResponse()
        {
            UserId = user.Id.ToString(),
            Avatar = user.Avatar,
            Socials = user.Socials?.Select(ToSocialResponse).ToList(),
            Username = user.Username,
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
            Banner = user.ProfileBackground,
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
            Genres = track.Genres.Select(ToGenreResponse),
            PerformerId = track.PerformerId,
            Url = track.Url,
        };
    }

    public static List<TrackResponse> ToTracksResponse(this List<Track> tracks)
    {
        return tracks.Select(ToTrackResponse).ToList();
    }

    public static FavoriteTrackResponse ToFavoriteTrackResponse(this Track track)
    {
        return new FavoriteTrackResponse
        {
            Id = track.Id.ToString(),
            Url = track.Url,
            Name = track.Name,
            Description = track.Description,
        };
    }

    public static List<FavoriteTrackResponse> ToFavoriteTracksResponse(this List<Track> tracks)
    {
        return tracks.Select(ToFavoriteTrackResponse).ToList();
    }

    public static ProducerRelatedPerformerResponse ToProducerRelatedPerformerResponse (this Performer performer)
    {
        return new ProducerRelatedPerformerResponse
        {
            BackgroundPhoto = performer.User?.ProfileBackground,
            Username = performer.User?.Username,
            Genres = performer.User?.Genres.Select(ToGenreResponse),
        };
    }

    public static SendInvitationPerformersResponse ToSendInvitationPerformersResponse(this List<Performer> performers)
    {
        return new()
        {
            Data = performers.Select(performer => performer.ToSendInvitationPerformerResponse())
        };
    }

    public static SendInvitationPerformerResponse ToSendInvitationPerformerResponse(this Performer performer)
    {
        var ratings = performer.User.ReceivedRatings;
        return new SendInvitationPerformerResponse()
        {
            UserId = performer.User.Id.ToString(),
            Avatar = performer.User.Avatar,
            Genres = performer.User.Genres?.Select(ToGenreResponse).ToList(),
            Username = performer.User.Username,
            Rating = ratings.Count != 0 ? ratings.Average(r => r.RatingValue) : 0
        };
    }
}
