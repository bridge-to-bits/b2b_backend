using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Performer
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
    public IEnumerable<Track> Tracks { get; set; }

    public ICollection<Producer> RelatedProducers { get; set; } = [];
}
