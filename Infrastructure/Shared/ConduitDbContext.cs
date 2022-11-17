using Domain.ArticleFeature.Models;
using Domain.UserFeature.Models;
using Infrastructure.ArticleFeature;
using Infrastructure.CommentFeature;
using Infrastructure.TagFeature;
using Infrastructure.UserFeature;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared;

public class ConduitDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ArticleEntity> Articles { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<UserFavouriteArticleEntity> UserFavouriteArticles { get; set; }
    public DbSet<UserFollowUserEntity> UserFollowUsers { get; set; }

    public ConduitDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>()
            .HasMany(tag => tag.Articles )
            .WithMany(article => article.Tags )
            .UsingEntity(join => join.ToTable("ArticleTag"));
        
        modelBuilder
            .Entity<ArticleEntity>()
            .Property(e => e.Slug)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        
        modelBuilder.Entity<UserFollowUserEntity>()
            .HasOne<UserEntity>(entity => entity.Follower)
            .WithMany(entity => entity.UserFollowsUsers)
            .HasForeignKey(entity => entity.FollowerId);
        
        modelBuilder.Entity<UserFollowUserEntity>()
            .HasOne<UserEntity>(entity => entity.Followed)
            .WithMany(entity => entity.UserFollowedByUsers)
            .HasForeignKey(entity => entity.FollowedId);
        
        modelBuilder.Entity<ArticleEntity>()
            .HasOne<UserEntity>(entity => entity.Author)
            .WithMany(entity => entity.Articles)
            .HasForeignKey(entity => entity.AuthorId);
        
        
        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder builder)
    {
        SeedUsers(builder);
        SeedArticles(builder);
        SeedTags(builder);
        SeedComments(builder);
    }

    private static void SeedComments(ModelBuilder builder)
    {
        List<CommentEntity> comments = new()
        {
            new() { Id = 1,Body = "Nice article", ArticleId = 1, AuthorId = 1},
            new() {Id = 2, Body = "bad article", ArticleId = 2, AuthorId = 2},
            new() {Id = 3, Body = "meh", ArticleId = 3, AuthorId = 3}
        };
        builder.Entity<CommentEntity>().HasData(comments);
    }

    private static void SeedTags(ModelBuilder builder)
    {
        List<TagEntity> tags = new()
        {
            new() {Id = 1, Name = "science" },
            new() {Id = 2, Name = "programming" }
        };
        builder.Entity<TagEntity>().HasData(tags);
    }

    private static void SeedArticles(ModelBuilder builder)
    {
        List<ArticleEntity> articles = new()
        {
            new() {Id = 1, Title = "c#", Body = "nice", Description = "interesting thumbnail", AuthorId = 1},
            new() {Id = 2, Title = "Java", Body = "nice", Description = "interesting thumbnail", AuthorId = 2 },
            new() {Id = 3, Title = "Python", Body = "nice", Description = "interesting thumbnail", AuthorId = 3 }
        };
        builder.Entity<ArticleEntity>().HasData(articles);
    }

    private static void SeedUsers(ModelBuilder builder)
    {
        List<UserEntity> users = new()
        {
            new() {Id = 1, Username = "shamel", Email = "shamel.com", Password = "12345678", Image = "", Bio = "" },
            new() {Id = 2, Username = "mohammed", Email = "Mohammed.com", Password = "12345678", Image = "", Bio = "" },
            new() {Id = 3, Username = "ahmad", Email = "Ahmad.com", Password = "12345678", Image = "", Bio = "" }
        };
        builder.Entity<UserEntity>().HasData(users);
    }
}