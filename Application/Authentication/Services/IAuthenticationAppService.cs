using Application.UserFeature;

namespace WebAPI.Authentication;

public interface IAuthenticationAppService
{
    Task Register(UserRequest user);
    Task<string> Login(UserLogin userLoginUser);


}