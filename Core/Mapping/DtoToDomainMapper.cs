using Core.DTOs.News;
using Core.DTOs.Users;
using Core.Models;

namespace Core.Mapping;
public static class DtoToDomainMapper
{
    public static User ToUser(this MainRegistrationDTO registrationDTO)
    {
        return new User
        {
            Email = registrationDTO.Email,
            Username = registrationDTO.Username,
            LastName = registrationDTO.LastName ?? "",
            UserType = registrationDTO.UserType,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }
    public static Social ToSocial(this AddSocialDTO addSocialDTO)
    {
        return new Social
        {
            Name = addSocialDTO.Name,
            Link = addSocialDTO.Link
        };
    }

    public static Genre ToGenre(this AddGenreDTO addGenreDTO)
    {
        return new Genre
        {
            Name = addGenreDTO.Name,
        };
    }

    public static Article ToArticle(this AddArticleDTO addArticleDTO)
    {
        return new Article
        {
            SenderId = addArticleDTO.SenderId,
            Title = addArticleDTO.Title,
            Content = addArticleDTO.Content,
            BackgroundPhotoUrl = addArticleDTO.BackgroundPhotoUrl,
            ContentPreview = addArticleDTO.ContentPreview,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static Interview ToInterview(this AddInterviewDTO addInterviewDTO)
    {
        return new Interview
        {
            SenderId = addInterviewDTO.SenderId,
            RespondentId = addInterviewDTO.RespondentId,
            VideoLink = addInterviewDTO.VideoLink,
            Title = addInterviewDTO.Title,
            Content = addInterviewDTO.Content,
            BackgroundPhotoUrl = addInterviewDTO.BackgroundPhotoUrl,
            ContentPreview = addInterviewDTO.ContentPreview,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
