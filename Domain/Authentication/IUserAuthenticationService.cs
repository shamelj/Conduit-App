using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Exceptions;
using Domain.UserFeature.Models;
using Domain.UserFeature.Services;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Authentication;

public interface IUserAuthenticationService
{
    Task<string> Login(User user);
}

internal class UserAuthenticationService : IUserAuthenticationService
{
    private readonly string _secret;
    private readonly IUserRepository _userRepository;
    private readonly double _minutesToLive;

    public UserAuthenticationService(IUserRepository userRepository, string secret, double minutesToLive)
    {
        _userRepository = userRepository;
        _secret = secret;
        _minutesToLive = minutesToLive;
    }

    public async Task<string> Login(User userCredintials)
    {
        var userDetails = await _userRepository.GetByUsername(userCredintials.Username) ?? throw new ConduitException
            { Message = "User not found", StatusCode = HttpStatusCode.NotFound };
        return CreateJwtToken(userDetails);
    }

    private string CreateJwtToken(User userDetails)
    {
        var claims = new List<Claim>
        {
            new("Username", userDetails.Username),
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