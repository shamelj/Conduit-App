using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public class UserService : BaseService<User, string>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) : base(userRepository)
    {
        _userRepository = userRepository;
    }
}