using Domain.ArticleFeature.Models;
using Domain.UserFeature.Models;
using Infrastructure.ArticleFeature;
using Infrastructure.CommentFeature;
using Infrastructure.TagFeature;
using Infrastructure.UserFeature;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-5V0QNET;Initial Catalog=Conduit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
    }
}