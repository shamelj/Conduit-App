using Application.UserFeature;

namespace Application.CommentFeature;

public class CommentResponse
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Body { get; set; }
    public ProfileResponse Author { get; set; }
}