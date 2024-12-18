using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class FavoriteTrack
{
    public Guid UserId { get; set; }
    public Guid TrackId { get; set; }


    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("PerformerId")]
    public Track Track { get; set; }
}
