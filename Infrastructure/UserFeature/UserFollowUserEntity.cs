using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UserFeature;

[Table("UserFollowUser")]
[Index(nameof(FollowerId), nameof(FollowedId), IsUnique = true)]
public class UserFollowUserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public UserEntity? Follower { get; set; }
    public UserEntity? Followed { get; set; }

    public long? FollowerId { get; set; }
    public long? FollowedId { get; set; }
    
}