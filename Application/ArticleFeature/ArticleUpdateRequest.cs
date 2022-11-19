using System.Text.RegularExpressions;

namespace Application.ArticleFeature;

public class ArticleUpdateRequest
{
    public string? Slug
    {
        get
        {
            var value = Title?
                .ToLower()
                .Trim()
                .Replace(' ', '-');

            // Replace all subsequent dashes with a single dash
            if (value != null)
            {
                value = Regex.Replace(value, @"[-]{2,}", "-");
            }
            return value;
        }
    }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Body { get; set; }
    public IEnumerable<string>? TagList { get; set; }
}