using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public interface IUserService : IService<User, string>
{
}