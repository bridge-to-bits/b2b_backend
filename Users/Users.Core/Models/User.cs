using System.ComponentModel.DataAnnotations;

namespace Users.Core.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(40)]
    public string Username { get; set; }

    public string Email { get; set; }

    [StringLength(255)]
    public string Password { get; set; }

    public int Age { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string City { get; set; }
    public string Avatar { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Role> Roles { get; set; }
    public Producer Producer { get; set; }
    public Performer Performer { get; set; }
    public ICollection<Rating> ReceivedRatings { get; set; }
    public ICollection<Rating> GivenRatings { get; set; }
}
