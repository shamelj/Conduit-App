using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public interface IUserRepository
{
    void Create(User user);
    Task Update(string username, User user);
    Task<User?> GetByUsername(string username);
    Task<bool> ExistsByUsername(string username);
    Task<bool> ExistsByEmail(string email);
}