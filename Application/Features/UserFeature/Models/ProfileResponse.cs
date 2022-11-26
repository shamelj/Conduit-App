namespace Application.Features.UserFeature.Models;

public class ProfileResponse
{
    public string Username { get; set; }

    public string Bio { get; set; }

    public string Image { get; set; }

    public bool Following { get; set; } = false;
}