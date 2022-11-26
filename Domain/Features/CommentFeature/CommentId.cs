namespace Domain.Features.CommentFeature;

public class CommentId
{
    public CommentId(long value)
    {
        Value = value;
    }

    public long Value { get; }
}