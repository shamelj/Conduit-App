using System.Net;
using Domain.Exceptions;
using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(User user)
    {
        if (await _userRepository.ExistsByUsername(user.Username))
            throw new ConduitException
                { Message = "Entered duplicated Username", StatusCode = HttpStatusCode.BadRequest };
        if (await _userRepository.ExistsByEmail(user.Email))
            throw new ConduitException
                { Message = "Entered duplicated Email", StatusCode = HttpStatusCode.BadRequest };
        _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await _userRepository.GetByUsername(username) ?? throw new ConduitException
            { Message = "No such username", StatusCode = HttpStatusCode.NotFound };
        return user;
    }

    public async Task<bool> IsFollowing(string follower, string followed)
    {
        return await _userRepository.IsFollowing(follower, followed);
    }

    public async Task FollowUser(string followerUsername, string followedUsername) //TODO should check follower?
    {
        if (!await _userRepository.ExistsByUsername(followedUsername))
            throw new ConduitException
                { Message = "No such username to follow", StatusCode = HttpStatusCode.NotFound };
        await _userRepository.FollowUser(followerUsername, followedUsername);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UnfollowUser(string followerUsername, string followedUsername)
    {
        if (!await _userRepository.ExistsByUsername(followedUsername))
            throw new ConduitException
                { Message = "No such username to Unfollow", StatusCode = HttpStatusCode.NotFound };
        await _userRepository.UnfollowUser(followerUsername, followedUsername);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(string username, User updatedUser)
    {
        if (!await _userRepository.ExistsByUsername(username))
            throw new ConduitException
                { Message = "No such username, make sure you're logged in", StatusCode = HttpStatusCode.NotFound };
        var hasUniqueId = !await _userRepository.ExistsByUsername(updatedUser.Username) ||
                          username.Equals(updatedUser.Username);
        if (!hasUniqueId)
            throw new ConduitException
                { Message = "Entered duplicated username", StatusCode = HttpStatusCode.BadRequest };
        await _userRepository.Update(username, updatedUser);
        await _unitOfWork.SaveChangesAsync();
    }
}