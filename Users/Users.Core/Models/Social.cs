using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Users.Core.Models;

public class Social
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
