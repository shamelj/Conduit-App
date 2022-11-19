﻿using Domain.Shared;

namespace Domain.UserFeature.Models;

public class User
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; } = string.Empty;

    public string Password { get; set; }

    public string Image { get; set; } = string.Empty;
    
}