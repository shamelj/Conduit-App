using Application.Authentication.Models;
using Application.Features.UserFeature.Models;

namespace Application.Authentication.Services;

public interface IAuthenticationAppService
{
    Task Register(UserRequest user);

    Task<string> Login(UserLogin userLoginUser);

    Task<UserResponse> GetUserByUsername(string username);
    Task<UserResponse> GetUserByEmail(string email);
    Task LogoutToken(string token);
}