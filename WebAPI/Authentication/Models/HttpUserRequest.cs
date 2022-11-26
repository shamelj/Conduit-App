using Application.Features.UserFeature.Models;

namespace WebAPI.Authentication.Models;

public class HttpUserRequest
{
    public UserRequest User { get; set; }
}