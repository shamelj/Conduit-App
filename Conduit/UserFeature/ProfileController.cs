using System.Security.Claims;
using Application.UserFeature;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.UserFeature;
[Route("/api/profiles")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IUserAppService _userService;
    // GET
    public ProfileController(IUserAppService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{username}")]
    public async Task<IActionResult> GetProfile([FromRoute] string username)
    {
        var authenticatedUsername = User.FindFirstValue("Username");
        var profile = await _userService.GetProfileByUsername(username,authenticatedUsername);
        return Ok(new { Profile = profile });
    }
    
    [HttpGet("{username}/follow")]
    public async Task<IActionResult> FollowProfile([FromRoute] string username)
    {
        var authenticatedUsername = User.FindFirstValue("Username");
        await _userService.FollowUser(authenticatedUsername,username);
        var profile = await _userService.GetProfileByUsername(username,authenticatedUsername);
        return Ok(new { Profile = profile });
    }
    [HttpDelete("{username}/follow")]
    public async Task<IActionResult> UnfollowProfile([FromRoute] string username)
    {
        var authenticatedUsername = User.FindFirstValue("Username");
        await _userService.UnfollowUser(authenticatedUsername,username);
        var profile = await _userService.GetProfileByUsername(username,authenticatedUsername);
        return Ok(new { Profile = profile });
    }
}