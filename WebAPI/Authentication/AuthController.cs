using Application.Authentication.Services;
using Application.Features.UserFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebAPI.Authentication.Models;
using WebAPI.Filters;

namespace WebAPI.Authentication;

[Route("api")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationAppService _authenticationAppService;
    private readonly IUserAppService _userAppService;

    public AuthController(IUserAppService userAppService, IAuthenticationAppService authenticationAppService)
    {
        _userAppService = userAppService;
        _authenticationAppService = authenticationAppService;
    }

    [HttpPost("users")]
    public async Task<IActionResult> Register([FromBody] HttpUserRequest userRequest)
    {
        await _authenticationAppService.Register(userRequest.User);
        var user = await _authenticationAppService.GetUserByUsername(userRequest.User.Username);
        return Ok(new { User = user });
    }


    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] HttpUserLogin userLogin)
    {
        var token = await _authenticationAppService.Login(userLogin.User);
        var user = await _authenticationAppService.GetUserByEmail(userLogin.User.Email);
        user.Token = token;
        return Ok(new { User = user });
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await _authenticationAppService.GetUserByUsername(User.Identity.Name);
        user.Token = GetCurrentToken();
        return Ok(new { User = user });
    }

    [HttpPut("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(HttpUserUpdateRequest userUpdateRequest)
    {
        var username = User.Identity.Name;
        await _authenticationAppService.UpdateUserAsync(username, userUpdateRequest.User);
        var user = await _authenticationAppService.GetUserByUsername(userUpdateRequest.User.Username ?? username);
        await _authenticationAppService.LogoutToken(GetCurrentToken());
        //user.Token = GetCurrentToken();
        return Ok(new { User = user });
    }

    [HttpPost("users/logout")]
    public async Task<IActionResult> Logout()
    {
        var token = GetCurrentToken();
        if (token == null || !User.Identity.IsAuthenticated)
            return Ok(new { Message = "You wasn't logged in" });
        await _authenticationAppService.LogoutToken(GetCurrentToken());
        return NoContent();
    }

    private string GetCurrentToken()
    {
        var authHeader = HttpContext.Request?.Headers["authorization"];
        return authHeader.Equals(StringValues.Empty) ? string.Empty : authHeader.Single<string>().Split(" ").Last();
    }
}