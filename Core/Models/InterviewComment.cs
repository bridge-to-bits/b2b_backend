using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class InterviewComment
{
    [Key]
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid InterviewId { get; set; }

    [ForeignKey(nameof(InterviewId))]
    public Interview Interview { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}
