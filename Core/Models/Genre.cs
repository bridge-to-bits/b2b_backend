using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Genre
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
    public IEnumerable<Track> Tracks { get; set; }
}
