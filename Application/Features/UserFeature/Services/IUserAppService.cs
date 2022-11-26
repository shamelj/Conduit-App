using Application.Features.UserFeature.Models;

namespace Application.Features.UserFeature.Services;

public interface IUserAppService
{
    Task Update(string username, UserRequest user);

    Task<ProfileResponse> GetProfileByUsernameAsync(string username, string? followedByUser = null);

    Task FollowUser(string followerUsername, string followedUsername);

    Task UnfollowUser(string followerUsername, string followedUsername);
    // Task GetProfileByEmailAsync(string email);
}