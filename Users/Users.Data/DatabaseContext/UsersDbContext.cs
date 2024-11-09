﻿using Microsoft.EntityFrameworkCore;
using Users.Core.Models;

namespace Users.Data.DatabaseContext;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get;set; }
    public DbSet<Grant> Grants { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Performer> Performers { get; set; }
    public DbSet<Rating> Ratings { get; set; }

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
    }
}