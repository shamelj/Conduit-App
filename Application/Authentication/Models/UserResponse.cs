﻿namespace Application.Authentication.Models;

public class UserResponse
{
    public string Email { get; set; }

    public string Token { get; set; }

    public string Username { get; set; }

    public string Bio { get; set; }

    public string Image { get; set; }
}