using Application.Features.UserFeature.Models;

namespace Application.Features.CommentFeature.Models;

public class CommentResponse
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Body { get; set; }
    public ProfileResponse Author { get; set; }
}