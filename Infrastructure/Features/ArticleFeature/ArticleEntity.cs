using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Shared;
using Infrastructure.Features.CommentFeature;
using Infrastructure.Features.TagFeature;
using Infrastructure.Features.UserFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.ArticleFeature;

[Table("Article")]
[Index(nameof(Slug), IsUnique = true)]
public class ArticleEntity
{
    private string? _slug;

    private string? _title;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Slug
    {
        get => _slug;
        private set
        {
            if (value != null) _slug = value.ToSlug();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Slug = value;
        }
    }

    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IEnumerable<TagEntity> Tags { get; set; }
    public IEnumerable<CommentEntity> Comments { get; set; }
    public UserEntity Author { get; set; }

    public List<UserFavouriteArticleEntity> UserFavouriteArticles { get; set; }

    public long AuthorId { get; set; }
}