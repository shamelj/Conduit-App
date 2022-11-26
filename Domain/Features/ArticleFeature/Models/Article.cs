using Domain.Shared;

namespace Domain.Features.ArticleFeature.Models;

public class Article
{
    private string? _slug;
    private string? _title;

    public string Slug
    {
        get => _slug;
        private set
        {
            if (value != null) _slug = value.ToSlug();
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