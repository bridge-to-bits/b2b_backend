﻿// <auto-generated />
using System;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(B2BDbContext))]
    [Migration("20241221190500_Add_Article_Models")]
    partial class Add_Article_Models
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Core.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BackgroundPhotoUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContentPreview")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Core.Models.ArticleComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleComment");
                });

            modelBuilder.Entity("Core.Models.ArticleRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("InitiatorId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("InitiatorId");

                    b.ToTable("ArticleRating");
                });

            modelBuilder.Entity("Core.Models.FavoritePerformer", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PerformerId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "PerformerId");

                    b.HasIndex("PerformerId");

                    b.ToTable("FavoritePerformers");
                });

            modelBuilder.Entity("Core.Models.FavoriteTrack", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TrackId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("FavoriteTracks");
                });

            modelBuilder.Entity("Core.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Core.Models.Grant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Grants");
                });

            modelBuilder.Entity("Core.Models.Performer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Performers");
                });

            modelBuilder.Entity("Core.Models.Producer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("Core.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("InitiatorUserId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<Guid>("TargetUserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("InitiatorUserId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Core.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Core.Models.Social", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Socials");
                });

            modelBuilder.Entity("Core.Models.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PerformerId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TotalListenings")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("WeeklyListeningsAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerformerId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AboutMe")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProfileBackground")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenreTrack", b =>
                {
                    b.Property<Guid>("GenresId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TracksId")
                        .HasColumnType("char(36)");

                    b.HasKey("GenresId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("GenreTrack");
                });

            modelBuilder.Entity("GenreUser", b =>
                {
                    b.Property<Guid>("GenresId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("char(36)");

                    b.HasKey("GenresId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GenreUser");
                });

            modelBuilder.Entity("PerformerProducer", b =>
                {
                    b.Property<Guid>("RelatedPerformersId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RelatedProducersId")
                        .HasColumnType("char(36)");

                    b.HasKey("RelatedPerformersId", "RelatedProducersId");

                    b.HasIndex("RelatedProducersId");

                    b.ToTable("ProducersPerformers", (string)null);
                });

            modelBuilder.Entity("Core.Models.Article", b =>
                {
                    b.HasOne("Core.Models.User", "User")
                        .WithMany("AuthoredArticles")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.ArticleComment", b =>
                {
                    b.HasOne("Core.Models.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.User", "User")
                        .WithMany("ArticleComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.ArticleRating", b =>
                {
                    b.HasOne("Core.Models.Article", "Article")
                        .WithMany("Ratings")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.User", "User")
                        .WithMany("ArticleRatings")
                        .HasForeignKey("InitiatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.FavoritePerformer", b =>
                {
                    b.HasOne("Core.Models.Performer", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.User", "User")
                        .WithMany("FavoritePerformers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Performer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.FavoriteTrack", b =>
                {
                    b.HasOne("Core.Models.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.User", "User")
                        .WithMany("FavoriteTracks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.Grant", b =>
                {
                    b.HasOne("Core.Models.Role", "Role")
                        .WithMany("Grants")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Core.Models.Performer", b =>
                {
                    b.HasOne("Core.Models.User", "User")
                        .WithOne("Performer")
                        .HasForeignKey("Core.Models.Performer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.Producer", b =>
                {
                    b.HasOne("Core.Models.User", "User")
                        .WithOne("Producer")
                        .HasForeignKey("Core.Models.Producer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.Rating", b =>
                {
                    b.HasOne("Core.Models.User", "InitiatorUser")
                        .WithMany("GivenRatings")
                        .HasForeignKey("InitiatorUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Models.User", "TargetUser")
                        .WithMany("ReceivedRatings")
                        .HasForeignKey("TargetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InitiatorUser");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("Core.Models.Role", b =>
                {
                    b.HasOne("Core.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.Social", b =>
                {
                    b.HasOne("Core.Models.User", "User")
                        .WithMany("Socials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Models.Track", b =>
                {
                    b.HasOne("Core.Models.Performer", "Performer")
                        .WithMany("Tracks")
                        .HasForeignKey("PerformerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Performer");
                });

            modelBuilder.Entity("GenreTrack", b =>
                {
                    b.HasOne("Core.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Track", null)
                        .WithMany()
                        .HasForeignKey("TracksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreUser", b =>
                {
                    b.HasOne("Core.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PerformerProducer", b =>
                {
                    b.HasOne("Core.Models.Performer", null)
                        .WithMany()
                        .HasForeignKey("RelatedPerformersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Producer", null)
                        .WithMany()
                        .HasForeignKey("RelatedProducersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.Article", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Core.Models.Performer", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Core.Models.Role", b =>
                {
                    b.Navigation("Grants");
                });

            modelBuilder.Entity("Core.Models.User", b =>
                {
                    b.Navigation("ArticleComments");

                    b.Navigation("ArticleRatings");

                    b.Navigation("AuthoredArticles");

                    b.Navigation("FavoritePerformers");

                    b.Navigation("FavoriteTracks");

                    b.Navigation("GivenRatings");

                    b.Navigation("Performer")
                        .IsRequired();

                    b.Navigation("Producer")
                        .IsRequired();

                    b.Navigation("ReceivedRatings");

                    b.Navigation("Roles");

                    b.Navigation("Socials");
                });
#pragma warning restore 612, 618
        }
    }
}
