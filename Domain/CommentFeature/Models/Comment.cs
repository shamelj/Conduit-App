using Domain.Shared;

namespace Domain.CommentFeature.Models;

public class Comment
{
    public long? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Body { get; set; } = string.Empty;

    public string Username { get; set; }
    public string ArticleSlug { get; set; }
}