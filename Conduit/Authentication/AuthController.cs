using Application.UserFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Authentication;

[Route("api")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class AuthController : ControllerBase
{
    private readonly IUserAppService _userAppService;
    private readonly IAuthenticationAppService _authenticationAppService;

    public AuthController(IUserAppService userAppService, IAuthenticationAppService authenticationAppService)
    {
        _userAppService = userAppService;
        _authenticationAppService = authenticationAppService;
    }

    [HttpPost("users")]
    public async Task<IActionResult> Register([FromBody] HttpUserRequest userRequest)
    {
        await _authenticationAppService.Register(userRequest.User);
        var user =await _userAppService.GetProfileByUsernameAsync(userRequest.User.Username);
        return Ok(new { User = user });
    }
    
    
    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] HttpUserLogin userLogin)
    {
        var token = await _authenticationAppService.Login(userLogin.User);
        // var user =await _userAppService.GetProfileByEmailAsync(userLogin.User.Email);
        return Ok(token);
    }
}