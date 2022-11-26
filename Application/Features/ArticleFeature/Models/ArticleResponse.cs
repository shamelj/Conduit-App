using Application.Features.UserFeature.Models;
using Domain.Shared;

namespace Application.Features.ArticleFeature.Models;

public class ArticleResponse
{
    public string Slug => Title.ToSlug();

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