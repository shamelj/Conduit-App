using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Features.ArticleFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.UserFeature;

[Table("UserFavouriteArticle")]
[Index(nameof(UserId), nameof(ArticleId), IsUnique = true)]
public class UserFavouriteArticleEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public UserEntity? User { get; set; }
    public ArticleEntity Article { get; set; }

    public long? UserId { get; set; }
    public long ArticleId { get; set; }
}