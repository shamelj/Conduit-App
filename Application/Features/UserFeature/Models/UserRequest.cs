namespace Application.Features.UserFeature.Models;

public class UserRequest
{
    //todo add validation
    public string Username { get; set; }

    public string Email { get; set; }

    public string? Bio { get; set; }

    public string Password { get; set; }

    public string? Image { get; set; }
}