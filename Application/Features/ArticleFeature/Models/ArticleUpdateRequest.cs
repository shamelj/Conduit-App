using Domain.Shared;

namespace Application.Features.ArticleFeature.Models;

public class ArticleUpdateRequest
{
    public string? Slug => Title?.ToSlug();

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Body { get; set; }
    public IEnumerable<string>? TagList { get; set; }
}