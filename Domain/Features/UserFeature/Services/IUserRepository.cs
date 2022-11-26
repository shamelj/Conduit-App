using Domain.Features.UserFeature.Models;

namespace Domain.Features.UserFeature.Services;

public interface IUserRepository
{
    void Create(User user);
    Task Update(string username, User user);
    Task<User?> GetByUsername(string username);
    Task<bool> ExistsByUsername(string username);
    Task<bool> ExistsByEmail(string email);
    Task<bool> IsFollowing(string follower, string followed);
    Task FollowUser(string followerUsername, string followedUsername);
    Task UnfollowUser(string followerUsername, string followedUsername);
    Task<User?> GetByEmail(string email);
    Task<bool> UserHasArticleAsync(string username, string slug);
    Task<bool> UserHasCommentAsync(string username, long commentId);
}