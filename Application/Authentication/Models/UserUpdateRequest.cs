namespace Application.Authentication.Models;

public class UserUpdateRequest
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Bio { get; set; } = string.Empty;

    public string? Password { get; set; }

    public string? Image { get; set; } = string.Empty;
}