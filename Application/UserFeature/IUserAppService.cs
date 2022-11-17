namespace Application.UserFeature;

public interface IUserAppService
{
    Task Register(UserRequest user);

    Task Update(string username, UserRequest user);

    Task<ProfileResponse> GetProfileByUsername(string username, string? authenticatedUsername = null);

    Task FollowUser(string followerUsername, string followedUsername);
    Task UnfollowUser(string followerUsername, string followedUsername);
}