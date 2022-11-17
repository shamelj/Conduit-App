using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Authentication;

[Route("api/users")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("/login")]
    public IActionResult Login([FromBody] UserLogin userAuth)
    {
        return null;
    }
}
