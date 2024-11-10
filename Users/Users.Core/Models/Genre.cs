using System.ComponentModel.DataAnnotations;

namespace Users.Core.Models;

public class Genre
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
}
