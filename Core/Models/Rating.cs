using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Rating
{
    [Key]
    public Guid Id { get; set; }
    public Guid TargetUserId { get; set; }
    public Guid InitiatorUserId { get; set; }
    [Range(1, 5)]
    public int RatingValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("InitiatorUserId")]
    public User InitiatorUser { get; set; }
    [ForeignKey("TargetUserId")]
    public User TargetUser { get; set; }
}
