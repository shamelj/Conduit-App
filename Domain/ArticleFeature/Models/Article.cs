using System.Text.RegularExpressions;
using Domain.ArticleFeature.Services;
using Domain.Shared;

namespace Domain.ArticleFeature.Models;

public class Article
{
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

    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public IEnumerable<string>? TagList { get; set; }
    public string Username { get; set; }
    
}