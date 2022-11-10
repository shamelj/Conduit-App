using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public interface IUserRepository : IRepository<User, string>
{
}