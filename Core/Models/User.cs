using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public string Email { get; set; }

    [StringLength(255)]
    public string Password { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(100)]
    public string Username { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }
    public string? Avatar { get; set; }
    public string? ProfileBackground { get; set; }

    [MaxLength(512)]
    public string? AboutMe { get; set; }
    public UserType UserType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Role> Roles { get; set; }
    public Producer Producer { get; set; }
    public Performer Performer { get; set; }
    public ICollection<Rating> ReceivedRatings { get; set; }
    public ICollection<Rating> GivenRatings { get; set; }
    public ICollection<Social>? Socials { get; set; }
    public ICollection<Genre>? Genres { get; set; }

    public ICollection<FavoritePerformer> FavoritePerformers { get; set; }
    public ICollection<FavoriteTrack> FavoriteTracks { get; set; }

    public ICollection<Article> AuthoredArticles { get; set; }
    public ICollection<ArticleComment> ArticleComments { get; set; }
    public ICollection<ArticleRating> ArticleRatings { get; set; }

    public ICollection<Interview> AuthoredInterviews { get; set; }
    public ICollection<Interview> RespondentedInterviews { get; set; }
    public ICollection<InterviewComment> InterviewComments { get; set; }
    public ICollection<InterviewRating> InterviewRatings { get; set; }
}
