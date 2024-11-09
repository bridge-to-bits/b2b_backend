using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Users.Core.Models;

public class Performer
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
