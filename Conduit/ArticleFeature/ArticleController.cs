using Application.ArticleFeature;
using Application.Authentication;
using Application.Authentication.Requirements;
using Domain.ArticleFeature.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.ArticleFeature;

[Route("/api/articles")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class ArticleController : ControllerBase
{
    private readonly IArticleAppService _articleService;
    private readonly IAuthorizationService _authorizationService;

    public ArticleController(IArticleAppService articleService, IAuthorizationService authenticationService)
    {
        _articleService = articleService;
        _authorizationService = authenticationService;
    }

    [HttpGet]
    public async Task<IActionResult> ListArticles(
        [FromQuery(Name = "author")] string? authorUsername,
        [FromQuery(Name = "tag")] string? tagName,
        [FromQuery(Name = "favorited")] string? favoritedUsername,
        [FromQuery(Name = "limit")] int limit = 20,
        [FromQuery(Name = "offset")] int offset = 0)
    {
        var authenticatedUsername = User.Identity?.Name;        var listArticlesRequestParams =
            CreateArticleRequestParams(authorUsername: authorUsername, tagName: tagName,
                favoritedUsername: favoritedUsername, limit: limit, offset: offset);
        var articles = await _articleService.ListArticlesAsync(authenticatedUsername, listArticlesRequestParams);
        var articleResponses = articles.ToList();
        return Ok(new { Articles = articleResponses, ArticlesCount = articleResponses.Count() });
    }

    private ListArticlesRequestParams CreateArticleRequestParams(string? feedForUser = "", string? authorUsername = "",
        string? tagName = "",
        string? favoritedUsername = "", int limit = 20, int offset = 0)
    {
        return new ListArticlesRequestParams
        {
            FeedForUser = feedForUser,
            AuthorUsername = authorUsername,
            TagName = tagName,
            FavoritedUsername = favoritedUsername,
            Limit = limit,
            Offset = offset
        };
    }

    [HttpGet("feed")]
    [Authorize]
    public async Task<IActionResult> ListFeed(
        [FromQuery(Name = "limit")] int limit = 20,
        [FromQuery(Name = "offset")] int offset = 0)
    {
        var authenticatedUsername = User.Identity?.Name;        var listArticlesRequestParams =
            CreateArticleRequestParams(authenticatedUsername, limit: limit, offset: offset);
        var articles = (await _articleService.ListArticlesAsync(authenticatedUsername, listArticlesRequestParams))
            .ToList();
        return Ok(new { Articles = articles, ArticlesCount = articles.Count() });
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.Identity?.Name;
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, slug);
        return Ok(new { article });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateArticle([FromBody] HttpArticleRequest articleRequest)
    {
        var authenticatedUsername = User.Identity?.Name;
        await _articleService.CreateArticle(articleRequest.Article, authenticatedUsername);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, articleRequest.Article.Slug);
        return Ok(new { article });
    }

    [HttpPut("{slug}")]
    [Authorize]
    public async Task<IActionResult> UpdateArticle([FromRoute] string slug,
        [FromBody] HttpArticleUpdateRequest articleRequest)
    {
        var authenticatedUsername = User.Identity?.Name;
        var authorizationResult =
            await _authorizationService.AuthorizeAsync(User, new Slug(slug), CrudRequirements.Update);
        if (authorizationResult.Succeeded)
        {
            await _articleService.UpdateArticle(articleRequest.Article, slug);
            var article =
                await _articleService.GetArticleBySlug(authenticatedUsername, articleRequest.Article.Slug ?? slug);
            return Ok(new { article });
        }

        return new ForbidResult();
    }

    [HttpDelete("{slug}"), Authorize]
    public async Task<IActionResult> DeleteArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.Identity?.Name;
        var authorizationResult =
            await _authorizationService.AuthorizeAsync(User, new Slug(slug), CrudRequirements.Delete);
        if (authorizationResult.Succeeded)
        {
            await _articleService.DeleteArticleAsync(slug);
            return NoContent();
        }
        return new ForbidResult();
    }

    // todo authenticate user
    [HttpPost("{slug}/favorite"), Authorize]
    public async Task<IActionResult> FavoriteArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.Identity?.Name;
        await _articleService.FavoriteArticleAsync(slug, authenticatedUsername);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, slug);
        return Ok(new { article });
    }

    // todo authenticate user
    [HttpDelete("{slug}/favorite"), Authorize]
    public async Task<IActionResult> UnfavoriteArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.Identity?.Name;
        await _articleService.UnfavoriteArticleAsync(slug, authenticatedUsername);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, slug);
        return Ok(new { article });
    }


    public class HttpArticleRequest
    {
        public ArticleRequest Article { get; set; }
    }

    public class HttpArticleUpdateRequest
    {
        public ArticleUpdateRequest Article { get; set; }
    }
}