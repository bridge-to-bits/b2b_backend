using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Article
{
    [Key]
    public Guid Id { get; set; }
    public string ContentPreview { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid SenderId { get; set; }
    public string BackgroundPhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(SenderId))]
    public User Author { get; set; }
    
    public ICollection<ArticleComment> Comments { get; set; }
    public ICollection<ArticleRating> Ratings { get; set; }
}
