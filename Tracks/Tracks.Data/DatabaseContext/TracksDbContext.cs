using Microsoft.EntityFrameworkCore;
using Tracks.Core.Models;

namespace Tracks.Data.DatabaseContext;

public class TracksDbContext(DbContextOptions<TracksDbContext> options) : DbContext(options)
{
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Genre> Genres { get;set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>()
            .HasMany(track => track.Genres)
            .WithMany(genre => genre.Tracks)
            .UsingEntity<Dictionary<string, object>>(
                "GenresTracks",
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                j => j.HasOne<Track>().WithMany().HasForeignKey("TrackId"));
    }
}