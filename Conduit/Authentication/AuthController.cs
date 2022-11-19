using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Authentication;

[Route("api/users")]
[ApiController]
[ConduitExceptionHandlerFilter]

public class AuthController : ControllerBase
{
    [HttpPost("/login")]
    public IActionResult Login([FromBody] UserLogin userAuth)
    {
        return null;
    }
}
