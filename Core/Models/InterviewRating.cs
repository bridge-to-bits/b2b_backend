using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class InterviewRating
{
    [Key]
    public Guid Id { get; set; }
    public double Value { get; set; }
    public Guid InitiatorId { get; set; }
    public Guid InterviewId { get; set; }

    [ForeignKey(nameof(InterviewId))]
    public Interview Interview { get; set; }

    [ForeignKey(nameof(InitiatorId))]
    public User User { get; set; }
}
