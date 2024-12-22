using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class ArticleRating
{
    [Key]
    public Guid Id { get; set; }
    public double Value { get; set; }
    public Guid InitiatorId { get; set; }
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; }

    [ForeignKey(nameof(InitiatorId))]
    public User User { get; set; }
}
