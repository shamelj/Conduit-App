using System.Net;
using Domain.Exceptions;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Create(User user)
    {
        if (await _userRepository.ExistsByUsername(user.UserName))
            throw new ConduitException
                { Message = "Entered duplicated Username", StatusCode = HttpStatusCode.BadRequest };
        if (await _userRepository.ExistsByEmail(user.Email))
            throw new ConduitException
                { Message = "Entered duplicated Email", StatusCode = HttpStatusCode.BadRequest };
        _userRepository.Create(user);
    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await _userRepository.GetByUsername(username) ?? throw new ConduitException
            { Message = "No such username", StatusCode = HttpStatusCode.NotFound };
        return user;
    }

    public async Task Update(string username, User updatedUser)
    {
        if (!await _userRepository.ExistsByUsername(username))
            throw new ConduitException
                { Message = "No such username, make sure you're logged in", StatusCode = HttpStatusCode.NotFound };
        var hasUniqueId = !await _userRepository.ExistsByUsername(updatedUser.UserName) ||
                          username.Equals(updatedUser.UserName);
        if (!hasUniqueId)
            throw new ConduitException
                { Message = "Entered duplicated username", StatusCode = HttpStatusCode.BadRequest };
        await _userRepository.Update(username, updatedUser);
    }
}