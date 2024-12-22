using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Interview
{
    [Key]
    public Guid Id { get; set; }
    public string ContentPreview { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string BackgroundPhotoUrl { get; set; }
    public string? VideoLink { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid SenderId { get; set; }
    public Guid RespondentId { get; set; }

    [ForeignKey(nameof(SenderId))]
    public User Author { get; set; }

    [ForeignKey(nameof(RespondentId))]
    public User Respondent { get; set; }

    public ICollection<InterviewComment> Comments { get; set; }
    public ICollection<InterviewRating> Ratings { get; set; }
}
