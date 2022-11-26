using Application.Authentication.Models;

namespace WebAPI.Authentication.Models;

public class HttpUserUpdateRequest
{
    public UserUpdateRequest User { get; set; }
}