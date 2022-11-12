using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Infrastructure.CommentFeature;
using Infrastructure.TagFeature;
using Infrastructure.UserFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ArticleFeature;

[Table("Article")]
[Index(nameof(Slug), IsUnique = true)]
public class ArticleEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    private string _slug;
    private string _title;

    public string Slug
    {
        get
        {
            return _slug;
        }
        private set
        {
            value = value
                .ToLower()
                .Trim()
                .Replace(' ', '-');

            // Replace all subsequent dashes with a single dash
            value = Regex.Replace(value, @"[-]{2,}", "-");
            _slug = value;
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
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IEnumerable<TagEntity> Tags { get; set; }
    public IEnumerable<CommentEntity> Comments { get; set; }
    public UserEntity Author { get; set; }
}