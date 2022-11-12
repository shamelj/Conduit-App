using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.ArticleFeature.Models;
using Infrastructure.ArticleFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UserFeature;
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