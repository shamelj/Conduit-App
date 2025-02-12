﻿using Application.Features.UserFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Features.UserFeature;

[Route("/api/profiles")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class ProfileController : ControllerBase
{
    private readonly IUserAppService _userService;

    public ProfileController(IUserAppService userService)
    {
        _userService = userService;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetProfile([FromRoute] string username)
    {
        var authenticatedUsername = User.Identity?.Name;
        var profile = await _userService.GetProfileByUsernameAsync(username, authenticatedUsername);
        return Ok(new { Profile = profile });
    }

    [HttpPost("{username}/follow")]
    [Authorize]
    public async Task<IActionResult> FollowProfile([FromRoute] string username)
    {
        var authenticatedUsername = User.Identity?.Name;
        await _userService.FollowUser(authenticatedUsername, username);
        var profile = await _userService.GetProfileByUsernameAsync(username, authenticatedUsername);
        return Ok(new { Profile = profile });
    }

    [HttpDelete("{username}/follow")]
    [Authorize]
    public async Task<IActionResult> UnfollowProfile([FromRoute] string username)
    {
        var authenticatedUsername = User.Identity?.Name;
        await _userService.UnfollowUser(authenticatedUsername, username);
        var profile = await _userService.GetProfileByUsernameAsync(username, authenticatedUsername);
        return Ok(new { Profile = profile });
    }
}