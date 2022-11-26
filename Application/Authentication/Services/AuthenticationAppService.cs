using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application.Authentication.Models;
using Application.Features.UserFeature.Models;
using Domain.Authentication;
using Domain.Features.UserFeature.Models;
using Domain.Features.UserFeature.Services;
using Domain.Shared.Exceptions;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Services;

public class AuthenticationAppService : IAuthenticationAppService
{
    private readonly ILogoutRepository _logoutRepository;
    private readonly double _minutesToLive;
    private readonly string _secret;
    private readonly IUserService _userService;

    public AuthenticationAppService(IUserService userService, string secret, double minutesToLive,
        ILogoutRepository logoutRepository)
    {
        _userService = userService;
        _secret = secret;
        _minutesToLive = minutesToLive;
        _logoutRepository = logoutRepository;
    }

    public async Task Register(UserRequest userRequest)
    {
        await _userService.Create(userRequest.Adapt<User>());
    }

    public async Task<string> Login(UserLogin userLogin)
    {
        var user = await _userService.GetByEmail(userLogin.Email);
        if (!user.Password.Equals(userLogin.Password))
            throw new ConduitException { Message = "Incorrect password", StatusCode = HttpStatusCode.BadRequest };
        var token = CreateJwtToken(user);
        return token;
    }

    public async Task<UserResponse> GetUserByUsername(string username)
    {
        var user = await _userService.GetByUsername(username);
        return user.Adapt<UserResponse>();
    }

    public async Task<UserResponse> GetUserByEmail(string email)
    {
        var user = await _userService.GetByEmail(email);
        return user.Adapt<UserResponse>();
    }

    public async Task LogoutToken(string token)
    {
        await _logoutRepository.Logout(token);
    }

    public async Task UpdateUserAsync(string username, UserUpdateRequest userUpdateRequest)
    {
        await _userService.Update(username, userUpdateRequest.Adapt<User>());
    }

    private string CreateJwtToken(User userDetails)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userDetails.Username),
            new(ClaimTypes.Email, userDetails.Email)
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(_minutesToLive),
            signingCredentials: creds);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}