using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Track
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }

    [Required]
    public string Url { get; set; }

    [Required]
    public Guid PerformerId { get; set; }

    [Required]
    public int WeeklyListeningsAmount { get; set; }

    [Required]
    public int TotalListenings {  get; set; }

    public ICollection<Genre> Genres { get; set; }

    [ForeignKey(nameof(PerformerId))]
    public Performer Performer { get; set; }
}
