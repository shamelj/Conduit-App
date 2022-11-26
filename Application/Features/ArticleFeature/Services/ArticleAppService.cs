using Application.Features.ArticleFeature.Models;
using Application.Features.UserFeature.Services;
using Domain.Features.ArticleFeature.Models;
using Domain.Features.ArticleFeature.Services;
using Mapster;

namespace Application.Features.ArticleFeature.Services;

public class ArticleAppService : IArticleAppService
{
    private readonly IArticleService _articleService;
    private readonly IUserAppService _userAppService;

    public ArticleAppService(IArticleService articleService, IUserAppService userAppService)
    {
        _articleService = articleService;
        _userAppService = userAppService;
    }

    public async Task<IEnumerable<ArticleResponse>> ListArticlesAsync(string favoritedByUser,
        ListArticlesRequestParams requestParams)
    {
        var articles = await _articleService.ListArticlesAsync(requestParams);
        var articlesResponse = articles.Select(article => ToArticleResponse(favoritedByUser, article).Result);
        return articlesResponse;
    }

    public async Task<ArticleResponse> GetArticleBySlug(string favoritedByUser, string slug)
    {
        var article = await _articleService.GetBySlugAsync(slug);
        var articleResponse = await ToArticleResponse(favoritedByUser, article);
        return articleResponse;
    }

    public async Task CreateArticle(ArticleRequest articleRequest, string createdByUser)
    {
        var article = ToArticle(articleRequest, createdByUser);
        await _articleService.CreateAsync(article);
    }

    public async Task UpdateArticle(ArticleUpdateRequest articleRequest, string slug)
    {
        var article = articleRequest.Adapt<Article>();
        await _articleService.UpdateAsync(slug, article);
    }

    public async Task DeleteArticleAsync(string slug)
    {
        await _articleService.DeleteBySlugAsync(slug);
    }

    public async Task FavoriteArticleAsync(string slug, string followingUsername)
    {
        await _articleService.FavoriteArticleAsync(slug, followingUsername);
    }

    public async Task UnfavoriteArticleAsync(string slug, string followingUsername)
    {
        await _articleService.UnfavoriteArticleAsync(slug, followingUsername);
    }

    private static Article ToArticle(ArticleRequest articleRequest, string createdByUser)
    {
        var article = articleRequest.Adapt<Article>();
        article.AuthorUsername = createdByUser;
        return article;
    }

    private async Task<ArticleResponse> ToArticleResponse(string favoritedByUser, Article article)
    {
        var articleResponse = article.Adapt<ArticleResponse>();
        articleResponse.Author =
            await _userAppService.GetProfileByUsernameAsync(article.AuthorUsername, favoritedByUser);
        articleResponse.Favorited = await _articleService.FavoritedByUser(article.Slug, favoritedByUser);
        articleResponse.FavoritesCount = await _articleService.CountFavorites(article.Slug);
        return articleResponse;
    }
}