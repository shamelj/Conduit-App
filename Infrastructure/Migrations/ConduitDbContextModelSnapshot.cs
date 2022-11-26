﻿// <auto-generated />
using System;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ConduitDbContext))]
    partial class ConduitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArticleEntityTagEntity", b =>
                {
                    b.Property<long>("ArticlesId")
                        .HasColumnType("bigint");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint");

                    b.HasKey("ArticlesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ArticleTag", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Features.ArticleFeature.ArticleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Article");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AuthorId = 1L,
                            Body = "elegant language",
                            Description = "interesting thumbnail",
                            Slug = "c-sharp",
                            Title = "C-sharp"
                        },
                        new
                        {
                            Id = 2L,
                            AuthorId = 2L,
                            Body = "very long body",
                            Description = "boring thumbnail",
                            Slug = "java",
                            Title = "Java"
                        },
                        new
                        {
                            Id = 3L,
                            AuthorId = 3L,
                            Body = "having a hard time",
                            Description = "",
                            Slug = "earth",
                            Title = "Earth"
                        },
                        new
                        {
                            Id = 4L,
                            AuthorId = 4L,
                            Body = "Do your best",
                            Description = "What you need to know",
                            Slug = "university-life",
                            Title = "University Life"
                        },
                        new
                        {
                            Id = 5L,
                            AuthorId = 5L,
                            Body = "Very delicious",
                            Description = "try it once love it forever",
                            Slug = "spaghetti",
                            Title = "Spaghetti"
                        });
                });

            modelBuilder.Entity("Infrastructure.Features.CommentFeature.CommentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Comment");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ArticleId = 2L,
                            AuthorId = 1L,
                            Body = "Nice article",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            ArticleId = 1L,
                            AuthorId = 2L,
                            Body = "bad article",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            ArticleId = 3L,
                            AuthorId = 3L,
                            Body = "good to know",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4L,
                            ArticleId = 4L,
                            AuthorId = 5L,
                            Body = "interesting",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5L,
                            ArticleId = 5L,
                            AuthorId = 3L,
                            Body = "looks delicious",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Infrastructure.Features.TagFeature.TagEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "legacy code"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "programming"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "nature"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "education"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "food"
                        });
                });

            modelBuilder.Entity("Infrastructure.Features.UserFeature.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Bio = "",
                            Email = "shamel.com",
                            Image = "",
                            Password = "12345678",
                            Username = "shamel"
                        },
                        new
                        {
                            Id = 2L,
                            Bio = "",
                            Email = "Mohammed.com",
                            Image = "",
                            Password = "12345678",
                            Username = "mohammed"
                        },
                        new
                        {
                            Id = 3L,
                            Bio = "",
                            Email = "Ahmad.com",
                            Image = "",
                            Password = "12345678",
                            Username = "ahmad"
                        },
                        new
                        {
                            Id = 4L,
                            Bio = "",
                            Email = "ali.com",
                            Image = "",
                            Password = "12345678",
                            Username = "ali"
                        },
                        new
                        {
                            Id = 5L,
                            Bio = "",
                            Email = "amal.com",
                            Image = "",
                            Password = "12345678",
                            Username = "amal"
                        });
                });

            modelBuilder.Entity("Infrastructure.Features.UserFeature.UserFavouriteArticleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId", "ArticleId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("UserFavouriteArticle");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ArticleId = 3L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ArticleId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            ArticleId = 4L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 4L,
                            ArticleId = 2L,
                            UserId = 3L
                        },
                        new
                        {
                            Id = 5L,
                            ArticleId = 3L,
                            UserId = 4L
                        });
                });

            modelBuilder.Entity("Infrastructure.Features.UserFeature.UserFollowUserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("FollowedId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long?>("FollowerId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FollowedId");

                    b.HasIndex("FollowerId", "FollowedId")
                        .IsUnique();

                    b.ToTable("UserFollowUser");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FollowedId = 2L,
                            FollowerId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            FollowedId = 3L,
                            FollowerId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            FollowedId = 4L,
                            FollowerId = 3L
                        },
                        new
                        {
                            Id = 4L,
                            FollowedId = 5L,
                            FollowerId = 4L
                        },
                        new
                        {
                            Id = 5L,
                            FollowedId = 1L,
                            FollowerId = 5L
                        });
                });

            modelBuilder.Entity("ArticleEntityTagEntity", b =>
                {
                    b.HasOne("Infrastructure.Features.ArticleFeature.ArticleEntity", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Features.TagFeature.TagEntity", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Features.ArticleFeature.ArticleEntity", b =>
                {
                    b.HasOne("Infrastructure.Features.UserFeature.UserEntity", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Infrastructure.Features.CommentFeature.CommentEntity", b =>
                {
                    b.HasOne("Infrastructure.Features.ArticleFeature.ArticleEntity", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Features.UserFeature.UserEntity", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Article");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Infrastructure.Features.UserFeature.UserFavouriteArticleEntity", b =>
                {
                    b.HasOne("Infrastructure.Features.ArticleFeature.ArticleEntity", "Article")
                        .WithMany("UserFavouriteArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Features.UserFeature.UserEntity", "User")
                        .WithMany("UserFavouriteArticles")
                        .HasForeignKey("UserId");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Features.UserFeature.UserFollowUserEntity", b =>
                {
                    b.HasOne("Infrastructure.Features.UserFeature.UserEntity", "Followed")
                        .WithMany("UserFollowedByUsers")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.Features.UserFeature.UserEntity", "Follower")
                        .WithMany("UserFollowsUsers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followed");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("Infrastructure.Features.ArticleFeature.ArticleEntity", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UserFavouriteArticles");
                });

            modelBuilder.Entity("Infrastructure.Features.UserFeature.UserEntity", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Comments");

                    b.Navigation("UserFavouriteArticles");

                    b.Navigation("UserFollowedByUsers");

                    b.Navigation("UserFollowsUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
