using Infrastructure.Features.ArticleFeature;
using Infrastructure.Features.CommentFeature;
using Infrastructure.Features.TagFeature;
using Infrastructure.Features.UserFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared;

public class ConduitDbContext : DbContext
{
    public ConduitDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ArticleEntity> Articles { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<UserFavouriteArticleEntity> UserFavouriteArticles { get; set; }
    public DbSet<UserFollowUserEntity> UserFollowUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>()
            .HasMany(tag => tag.Articles)
            .WithMany(article => article.Tags)
            .UsingEntity(join => join.ToTable("ArticleTag"));

        modelBuilder
            .Entity<ArticleEntity>()
            .Property(e => e.Slug)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        modelBuilder.Entity<UserFollowUserEntity>()
            .HasOne(entity => entity.Follower)
            .WithMany(entity => entity.UserFollowsUsers)
            .HasForeignKey(entity => entity.FollowerId);

        modelBuilder.Entity<UserFollowUserEntity>()
            .HasOne(entity => entity.Followed)
            .WithMany(entity => entity.UserFollowedByUsers)
            .HasForeignKey(entity => entity.FollowedId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ArticleEntity>()
            .HasOne(entity => entity.Author)
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
            new CommentEntity { Id = 1, Body = "Nice article", ArticleId = 2, AuthorId = 1 },
            new CommentEntity { Id = 2, Body = "bad article", ArticleId = 1, AuthorId = 2 },
            new CommentEntity { Id = 3, Body = "good to know", ArticleId = 3, AuthorId = 3 },
            new CommentEntity { Id = 4, Body = "interesting", ArticleId = 4, AuthorId = 5 },
            new CommentEntity { Id = 5, Body = "looks delicious", ArticleId = 5, AuthorId = 3 }
        };
        builder.Entity<CommentEntity>().HasData(comments);
    }

    private static void SeedTags(ModelBuilder builder)
    {
        List<TagEntity> tags = new()
        {
            new TagEntity { Id = 1, Name = "legacy code" },
            new TagEntity { Id = 2, Name = "programming" },
            new TagEntity { Id = 3, Name = "nature" },
            new TagEntity { Id = 4, Name = "education" },
            new TagEntity { Id = 5, Name = "food" }
        };
        builder.Entity<TagEntity>().HasData(tags);
    }

    private static void SeedArticles(ModelBuilder builder)
    {
        List<ArticleEntity> articles = new()
        {
            new ArticleEntity
            {
                Id = 1, Title = "C-sharp", Body = "elegant language", Description = "interesting thumbnail",
                AuthorId = 1
            },
            new ArticleEntity
                { Id = 2, Title = "Java", Body = "very long body", Description = "boring thumbnail", AuthorId = 2 },
            new ArticleEntity
                { Id = 3, Title = "Earth", Body = "having a hard time", Description = "", AuthorId = 3 },
            new ArticleEntity
            {
                Id = 4, Title = "University Life", Body = "Do your best", Description = "What you need to know",
                AuthorId = 4
            },
            new ArticleEntity
            {
                Id = 5, Title = "Spaghetti", Body = "Very delicious", Description = "try it once love it forever",
                AuthorId = 5
            }
        };
        builder.Entity<ArticleEntity>().HasData(articles);
    }

    private static void SeedUsers(ModelBuilder builder)
    {
        List<UserEntity> users = new()
        {
            new UserEntity
                { Id = 1, Username = "shamel", Email = "shamel.com", Password = "12345678", Image = "", Bio = "" },
            new UserEntity
                { Id = 2, Username = "mohammed", Email = "Mohammed.com", Password = "12345678", Image = "", Bio = "" },
            new UserEntity
                { Id = 3, Username = "ahmad", Email = "Ahmad.com", Password = "12345678", Image = "", Bio = "" },
            new UserEntity
                { Id = 4, Username = "ali", Email = "ali.com", Password = "12345678", Image = "", Bio = "" },
            new UserEntity
                { Id = 5, Username = "amal", Email = "amal.com", Password = "12345678", Image = "", Bio = "" }
        };
        builder.Entity<UserEntity>().HasData(users);

        List<UserFollowUserEntity> userFollowUserEntities = new()
        {
            new UserFollowUserEntity { Id = 1, FollowerId = 1, FollowedId = 2 },
            new UserFollowUserEntity { Id = 2, FollowerId = 2, FollowedId = 3 },
            new UserFollowUserEntity { Id = 3, FollowerId = 3, FollowedId = 4 },
            new UserFollowUserEntity { Id = 4, FollowerId = 4, FollowedId = 5 },
            new UserFollowUserEntity { Id = 5, FollowerId = 5, FollowedId = 1 }
        };
        builder.Entity<UserFollowUserEntity>().HasData(userFollowUserEntities);

        List<UserFavouriteArticleEntity> userFavouriteArticleEntities = new()
        {
            new UserFavouriteArticleEntity { Id = 1, UserId = 1, ArticleId = 3 },
            new UserFavouriteArticleEntity { Id = 2, UserId = 1, ArticleId = 1 },
            new UserFavouriteArticleEntity { Id = 3, UserId = 2, ArticleId = 4 },
            new UserFavouriteArticleEntity { Id = 4, UserId = 3, ArticleId = 2 },
            new UserFavouriteArticleEntity { Id = 5, UserId = 4, ArticleId = 3 }
        };
        builder.Entity<UserFavouriteArticleEntity>().HasData(userFavouriteArticleEntities);
    }
}