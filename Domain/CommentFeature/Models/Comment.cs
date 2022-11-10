using Domain.Shared;

namespace Domain.CommentFeature.Models;

public class Comment : IBaseModel<long>
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Body { get; set; } = string.Empty;

    public long GetId()
    {
        return Id;
    }
}