using System.Text.RegularExpressions;
using Domain.ArticleFeature.Services;
using Domain.Shared;

namespace Domain.ArticleFeature.Models;

public class Article
{
    private string? _title;
    private string? _slug;

    public string Slug
    {
        get => _slug;
        private set
        {
            if (value != null)
            {
                value = value
                    .ToLower()
                    .Trim()
                    .Replace(' ', '-');

                // Replace all subsequent dashes with a single dash
                value = Regex.Replace(value, @"[-]{2,}", "-");
                _slug = value;
            }
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Slug = value;
        }
    }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IEnumerable<string>? TagList { get; set; }
    public string AuthorUsername { get; set; }
    
}