using Application.Features.ArticleFeature.Models;
using Domain.Features.ArticleFeature.Models;

namespace Application.Features.ArticleFeature.Services;

public interface IArticleAppService
{
    Task<IEnumerable<ArticleResponse>> ListArticlesAsync(string favoritedByUser,
        ListArticlesRequestParams requestParams);

    Task<ArticleResponse> GetArticleBySlug(string favoritedByUser, string slug);
    Task CreateArticle(ArticleRequest articleRequest, string createdByUser);
    Task UpdateArticle(ArticleUpdateRequest articleRequest, string slug);

    Task DeleteArticleAsync(string slug);
    Task FavoriteArticleAsync(string slug, string followingUsername);
    Task UnfavoriteArticleAsync(string slug, string followingUsername);
}