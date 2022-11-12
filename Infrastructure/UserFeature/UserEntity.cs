using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.ArticleFeature;
using Infrastructure.CommentFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UserFeature;

[Table("User")]
[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] public string Username { get; set; }

    [Required] public string Email { get; set; }

    public string? Bio { get; set; }

    [Required] public string Password { get; set; }

    public string? Image { get; set; }

    public IEnumerable<ArticleEntity> Articles { get; set; }
    public IEnumerable<CommentEntity> Comments { get; set; }
    public IEnumerable<UserFavouriteArticleEntity> UserFavouriteArticles { get; set; }
    public IEnumerable<UserFollowUserEntity> UserFollowsUsers { get; set; }
    public IEnumerable<UserFollowUserEntity> UserFollowedByUsers { get; set; }


}