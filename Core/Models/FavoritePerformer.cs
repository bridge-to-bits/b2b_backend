using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class FavoritePerformer
{
    public Guid UserId { get; set; }
    public Guid PerformerId { get; set; }


    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("PerformerId")]
    public Performer Performer { get; set; }
}
