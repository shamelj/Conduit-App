using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public interface IUserService
{
    Task Create(User user);
    Task Update(string username, User user);
    Task<User> GetByUsername(string username);
}