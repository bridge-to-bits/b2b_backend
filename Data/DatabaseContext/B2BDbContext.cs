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
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleComment> ArticleComments { get; set; }
    public DbSet<ArticleRating> ArticleRatings { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<InterviewComment> InterviewComments { get; set; }
    public DbSet<InterviewRating> InterviewRatings { get; set; }


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

        modelBuilder.Entity<Producer>()
            .HasMany(p => p.RelatedPerformers)
            .WithMany(pf => pf.RelatedProducers)
            .UsingEntity(j => j.ToTable("ProducersPerformers"));

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

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasOne(a => a.Author)
                .WithMany(u => u.AuthoredArticles)
                .HasForeignKey(a => a.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Ratings)
                .WithOne(r => r.Article)
                .HasForeignKey(r => r.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ArticleComment>(entity =>
        {
            entity.HasOne(ac => ac.User)
                .WithMany(u => u.ArticleComments)
                .HasForeignKey(ac => ac.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ArticleRating>(entity =>
        {
            entity.HasOne(ar => ar.User)
                .WithMany(u => u.ArticleRatings)
                .HasForeignKey(ar => ar.InitiatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasOne(a => a.Author)
                .WithMany(u => u.AuthoredInterviews)
                .HasForeignKey(a => a.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Respondent)
                .WithMany(u => u.RespondentedInterviews)
                .HasForeignKey(a => a.RespondentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Comments)
                .WithOne(c => c.Interview)
                .HasForeignKey(c => c.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Ratings)
                .WithOne(r => r.Interview)
                .HasForeignKey(r => r.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InterviewComment>(entity =>
        {
            entity.HasOne(ac => ac.User)
                .WithMany(u => u.InterviewComments)
                .HasForeignKey(ac => ac.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InterviewRating>(entity =>
        {
            entity.HasOne(ar => ar.User)
                .WithMany(u => u.InterviewRatings)
                .HasForeignKey(ar => ar.InitiatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}