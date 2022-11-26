using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Features.ArticleFeature;
using Infrastructure.Features.UserFeature;

namespace Infrastructure.Features.CommentFeature;

[Table("Comment")]
public class CommentEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Body { get; set; }

    public ArticleEntity Article { get; set; }

    public UserEntity? Author { get; set; }

    public long? AuthorId { get; set; }

    public long ArticleId { get; set; }
}