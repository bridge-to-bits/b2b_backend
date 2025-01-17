using Core.Models;
using Core.Responses;
using Core.Responses.News;
using Core.Responses.Performers;
using Core.Responses.Tracks;
using Core.Responses.Users;

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
            UserId = performer.UserId.ToString(),
            BackgroundPhoto = performer.User?.ProfileBackground,
            Username = performer.User?.Username,
            Genres = performer.User?.Genres.Select(ToGenreResponse),
        };
    }

    public static PerformersResponse ToSendInvitationPerformersResponse(this List<Performer> performers)
    {
        return new()
        {
            Data = performers.Select(performer => performer.ToPerformerRespponse())
        };
    }

    public static PerformerResponse ToPerformerRespponse(this Performer performer)
    {
        var bestTrack = new TrackShortResponse();
        if (performer.Tracks != null && performer.Tracks.Any()) {
            var track = performer.Tracks.OrderByDescending(track => track.TotalListenings).First();
            bestTrack.Url = track.Url;
            bestTrack.Id = track.Id.ToString();
            bestTrack.Name = track.Name;
        }

        return new PerformerResponse()
        {
            Id = performer.User.Id.ToString(),
            Avatar = performer.User.Avatar,
            Genres = performer.User.Genres?.Select(ToGenreResponse),
            Socials = performer.User.Socials?.Select(ToSocialResponse),
            Username = performer.User.Username,
            Rating = performer.User.GetAvgRating(),
            Track = bestTrack,
        };
    }

    private static double GetAvgRating(this User user)
    {
        if(user.ReceivedRatings == null) return 0;
        var ratings = user.ReceivedRatings;
        return ratings.Count != 0 ? ratings.Average(r => r.RatingValue) : 0;
    } 

    public static WeeklyArticleResponse ToWeeklyArticleResponse(this Article article)
    {
        return new WeeklyArticleResponse()
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            ContentPreview = article.ContentPreview,
            BackgroundPhotoUrl = article.BackgroundPhotoUrl,
        };
    }

    public static WeeklyArticlesResponse ToWeeklyArticlesResponse(this IEnumerable<Article> articles)
    {
        return new WeeklyArticlesResponse()
        {
            Data = articles.Select(ToWeeklyArticleResponse),
        };
    }

    public static ArticleResponse ToArticleResponse(this Article article)
    {
        var ratings = article.Ratings;
        var rating = ratings.Count != 0 ? ratings.Average(r => r.Value) : 0;

        return new ArticleResponse()
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            ContentPreview = article.ContentPreview,
            BackgroundPhotoUrl = article.BackgroundPhotoUrl,
            Author = article.Author.ToNewsAuthorResponse(),
            Rating = rating,
            CreatedAt = article.CreatedAt,
            Comments = article.Comments.Select(ToArticleCommentResponse),
        };
    }

    private static NewsAuthorResponse ToNewsAuthorResponse(this User user) 
    {
        if (user == null) 
        {
            return new();
        }
        var rating = user == null ? 0 : user.GetAvgRating();
        return new NewsAuthorResponse()
        {
            AvatarUrl = user?.Avatar,
            Rating = rating,
            Username = user?.Username,
        };
    }

    private static NewsComentatorResponse ToNewsComentatorResponse(this User user)
    {
        return new NewsComentatorResponse()
        {
            AvatarUrl = user.Avatar,
            Username = user.Username,
        };
    }

    private static NewsCommentResponse ToArticleCommentResponse(this ArticleComment comment)
    {
        return new NewsCommentResponse()
        {
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            Id = comment.Id,
            Comentator = comment.User.ToNewsComentatorResponse(),
        };
    }

    public static WeeklyInterviewResponse ToWeeklyInterviewResponse(this Interview interview)
    {
        return new WeeklyInterviewResponse()
        {
            Id = interview.Id,
            Title = interview.Title,
            Content = interview.Content,
            ContentPreview = interview.ContentPreview,
            BackgroundPhotoUrl = interview.BackgroundPhotoUrl,
        };
    }

    public static WeeklyInterviewsResponse ToWeeklyInterviewsResponse(this IEnumerable<Interview> interviews)
    {
        return new WeeklyInterviewsResponse()
        {
            Data = interviews.Select(ToWeeklyInterviewResponse),
        };
    }

    public static InterviewResponse ToInterviewResponse(this Interview interview)
    {
        var ratings = interview.Ratings;
        var rating = ratings.Count != 0 ? ratings.Average(r => r.Value) : 0;

        return new InterviewResponse()
        {
            Id = interview.Id,
            Title = interview.Title,
            Content = interview.Content,
            ContentPreview = interview.ContentPreview,
            BackgroundPhotoUrl = interview.BackgroundPhotoUrl,
            Author = interview.Author.ToNewsAuthorResponse(),
            Respondent = interview.Respondent.ToNewsAuthorResponse(),
            VideoLink = interview.VideoLink,
            Rating = rating,
            CreatedAt = interview.CreatedAt,
            Comments = interview.Comments.Select(ToInterviewCommentResponse),
        };
    }

    private static NewsCommentResponse ToInterviewCommentResponse(this InterviewComment comment)
    {
        return new NewsCommentResponse()
        {
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            Id = comment.Id,
            Comentator = comment.User.ToNewsComentatorResponse(),
        };
    }

    public static PerformersResponse ToPerformersResponse(this UsersResponse users)
    {
        return new PerformersResponse()
        {
            CurrentPage = users.CurrentPage,
            NextPage = users.NextPage,
            PrevPage = users.PrevPage,
            TotalPages = users.TotalPages,
            Data = users.Data.Select(ToPerformerResponse),
        };
    }

    private static PerformerResponse ToPerformerResponse(this UserInfoResponse userInfoResponse)
    {
        return new PerformerResponse() { 
            Avatar = userInfoResponse.Avatar,
            Genres = userInfoResponse.Genres,
            Socials = userInfoResponse.Socials,
            Id = userInfoResponse.Id,
            Rating = userInfoResponse.Rating,
            Username = userInfoResponse.Username,
        };
    }
}
