using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Data.DatabaseContext;

public class B2BDbContext(DbContextOptions<B2BDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Grant> Grants { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Performer> Performers { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Social> Socials { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<FavoritePerformer> FavoritePerformers { get; set; }
    public DbSet<FavoriteTrack> FavoriteTracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Grants)
            .WithOne(g => g.Role)
            .HasForeignKey(g => g.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Producer)
            .WithOne(p => p.User)
            .HasForeignKey<Producer>(p => p.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Performer)
            .WithOne(p => p.User)
            .HasForeignKey<Performer>(p => p.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.UserType)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.TargetUser)
            .WithMany(u => u.ReceivedRatings)
            .HasForeignKey(r => r.TargetUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.InitiatorUser)
            .WithMany(u => u.GivenRatings)
            .HasForeignKey(r => r.InitiatorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Social>()
            .HasOne(s => s.User)
            .WithMany(u => u.Socials)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Genre>()
            .HasMany(g => g.Users)
            .WithMany(u => u.Genres);


        modelBuilder.Entity<Track>()
            .HasMany(track => track.Genres)
            .WithMany(genre => genre.Tracks);

        modelBuilder.Entity<Performer>()
            .HasMany(performer => performer.Tracks)
            .WithOne(track => track.Performer);

        modelBuilder.Entity<FavoritePerformer>()
            .HasKey(fp => new { fp.UserId, fp.PerformerId });

        modelBuilder.Entity<FavoritePerformer>()
            .HasOne(fp => fp.User)
            .WithMany(u => u.FavoritePerformers)
            .HasForeignKey(fp => fp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FavoritePerformer>()
            .HasOne(fp => fp.Performer)
            .WithMany()
            .HasForeignKey(fp => fp.PerformerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FavoriteTrack>()
            .HasKey(fp => new { fp.UserId, fp.TrackId });

        modelBuilder.Entity<FavoriteTrack>()
            .HasOne(fp => fp.User)
            .WithMany(u => u.FavoriteTracks)
            .HasForeignKey(fp => fp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FavoriteTrack>()
            .HasOne(fp => fp.Track)
            .WithMany()
            .HasForeignKey(fp => fp.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}