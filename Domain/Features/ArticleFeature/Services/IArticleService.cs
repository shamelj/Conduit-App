using Domain.Features.ArticleFeature.Models;

namespace Domain.Features.ArticleFeature.Services;

public interface IArticleService
{
    Task CreateAsync(Article article);
    Task<Article> GetBySlugAsync(string slug);
    Task UpdateAsync(string originalSlug, Article article);
    Task DeleteBySlugAsync(string slug);
    Task<IEnumerable<Article>> ListArticlesAsync(ListArticlesRequestParams requestParams);
    Task<bool> FavoritedByUser(string slug, string username);
    Task<int> CountFavorites(string slug);
    Task FavoriteArticleAsync(string slug, string followingUsername);
    Task UnfavoriteArticleAsync(string slug, string followingUsername);
}