namespace Domain.Features.UserFeature.Models;

public class User
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; } = string.Empty;

    public string Password { get; set; }

    public string Image { get; set; } = string.Empty;
}