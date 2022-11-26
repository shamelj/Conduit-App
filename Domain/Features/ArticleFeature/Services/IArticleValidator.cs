namespace Domain.Features.ArticleFeature.Services;

public interface IArticleValidator
{
    Task SlugMustBeUniqueAsync(string slug);
    Task ArticleMustExistBySlugAsync(string slug);
    Task UserMustExistAsync(string username);
}