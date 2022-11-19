using Application.UserFeature;
using Domain.ArticleFeature.Models;
using Domain.ArticleFeature.Services;
using Mapster;

namespace Application.ArticleFeature;

public class ArticleAppService : IArticleAppService
{
    private readonly IArticleService _articleService;
    private readonly IUserAppService _userAppService;

    public ArticleAppService(IArticleService articleService, IUserAppService userAppService)
    {
        _articleService = articleService;
        _userAppService = userAppService;
    }

    public async Task<IEnumerable<ArticleResponse>> ListArticlesAsync(string favoritedByUser, ListArticlesRequestParams requestParams)
    {
        IEnumerable<Article> articles =  await _articleService.ListArticlesAsync(requestParams);
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

    private static Article ToArticle(ArticleRequest articleRequest, string createdByUser)
    {
        var article = articleRequest.Adapt<Article>();
        article.AuthorUsername = createdByUser;
        return article;
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

    private async Task<ArticleResponse> ToArticleResponse(string favoritedByUser, Article article)
    {
        var articleResponse = article.Adapt<ArticleResponse>();
        articleResponse.Author = await _userAppService.GetProfileByUsernameAsync(article.AuthorUsername, favoritedByUser);
        articleResponse.Favorited = await _articleService.FavoritedByUser(article.Slug ,favoritedByUser);
        articleResponse.FavoritesCount = await _articleService.CountFavorites(article.Slug);
        return articleResponse;
    }
}