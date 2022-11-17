using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public interface IUserService
{
    Task Create(User user);
    Task Update(string username, User user);
    Task<User> GetByUsername(string username);
    Task<bool> IsFollowing(string follower, string followed);
    Task FollowUser(string followerUsername, string followedUsername);
    Task UnfollowUser(string followerUsername, string followedUsername);
}