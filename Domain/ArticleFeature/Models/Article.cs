using System.Text.RegularExpressions;
using Domain.Shared;

namespace Domain.ArticleFeature.Models;

public class Article : IBaseModel<string>
{
    public string Username { get; set; }

    public string Slug
    {
        get
        {
            var value = Title
                .ToLower()
                .Trim()
                .Replace(' ', '-');

            // Replace all subsequent dashes with a single dash
            value = Regex.Replace(value, @"[-]{2,}", "-");
            return value;
        }
    }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string GetId()
    {
        return Slug;
    }
}