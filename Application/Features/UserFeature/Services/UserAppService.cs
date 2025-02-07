﻿using Application.Features.UserFeature.Models;
using Domain.Features.UserFeature.Models;
using Domain.Features.UserFeature.Services;
using Mapster;

namespace Application.Features.UserFeature.Services;

public class UserAppService : IUserAppService
{
    private readonly IUserService _userService;

    public UserAppService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Update(string username, UserRequest user)
    {
        await _userService.Update(username, user.Adapt<User>());
    }

    public async Task<ProfileResponse> GetProfileByUsernameAsync(string profileUsername,
        string? authenticatedUsername = null)
    {
        var user = await _userService.GetByUsername(profileUsername);
        var profile = user.Adapt<ProfileResponse>();
        if (authenticatedUsername != null)
            profile.Following = await _userService.IsFollowing(authenticatedUsername, profileUsername);
        return profile;
    }

    public async Task FollowUser(string followerUsername, string followedUsername)
    {
        await _userService.FollowUser(followerUsername, followedUsername);
    }

    public async Task UnfollowUser(string followerUsername, string followedUsername)
    {
        await _userService.UnfollowUser(followerUsername, followedUsername);
    }

    // public async Task GetProfileByEmailAsync(string email)
    // {
    //     var user = await _userService.GetByEmail(email);
    //     var profile = user.Adapt<ProfileResponse>();
    //     if (authenticatedUsername != null)
    //         profile.Following = await _userService.IsFollowing(authenticatedUsername, profileUsername);
    //     return profile;
    // }
}