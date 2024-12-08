using System.ComponentModel.DataAnnotations;

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
    public IEnumerable<Genre> Genres { get; set; }
}
