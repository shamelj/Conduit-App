namespace Domain.Features.ArticleFeature;

public class Slug
{
    public Slug(string value)
    {
        Value = value;
    }

    public string Value { get; init; }
}