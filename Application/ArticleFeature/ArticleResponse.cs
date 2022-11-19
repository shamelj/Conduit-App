using System.Text.RegularExpressions;
using Application.UserFeature;

namespace Application.ArticleFeature;

public class ArticleResponse
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
    public IEnumerable<string>? TagList { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Favorited { get; set; }
    public int FavoritesCount { get; set; }
    public ProfileResponse Author { get; set; }
}