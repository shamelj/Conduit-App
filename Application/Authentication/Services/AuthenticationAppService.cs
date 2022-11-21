using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application.UserFeature;
using Domain.Exceptions;
using Domain.UserFeature.Models;
using Domain.UserFeature.Services;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication;

namespace Application.Authentication.Services;

public class AuthenticationAppService : IAuthenticationAppService
{
    private readonly IUserService _userService;
    
    private readonly string _secret;
    private readonly double _minutesToLive;

    public AuthenticationAppService(IUserService userService, string secret, double minutesToLive)
    {
        _userService = userService;
        _secret = secret;
        _minutesToLive = minutesToLive;
    }

    public async Task Register(UserRequest userRequest)
    {
        await _userService.Create(userRequest.Adapt<User>());
    }

    public async Task<string> Login(UserLogin userLogin)
    {
        User user = await _userService.GetByEmail(userLogin.Email);
        if (!user.Password.Equals(userLogin.Password))
            throw new ConduitException { Message = "Incorrect password", StatusCode = HttpStatusCode.BadRequest };
        string token = CreateJwtToken(user);
        return token;
    }

    private string CreateJwtToken(User userDetails)
    {
        var claims = new List<Claim>
        {
            new( ClaimTypes.Name, userDetails.Username),
            new("Email", userDetails.Email),
            new("Bio", userDetails.Bio),
            new("Image", userDetails.Image),
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