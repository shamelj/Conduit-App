using WebAPI.Authentication;
namespace Application.UserFeature;

public interface IUserAppService
{

    Task Update(string username, UserRequest user);

    Task<ProfileResponse> GetProfileByUsernameAsync(string username, string? authenticatedUsername = null);

    Task FollowUser(string followerUsername, string followedUsername);
    Task UnfollowUser(string followerUsername, string followedUsername);
    // Task GetProfileByEmailAsync(string email);
}