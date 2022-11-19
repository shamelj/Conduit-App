﻿using System.Security.Claims;
using Application.ArticleFeature;
using Domain.ArticleFeature.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.ArticleFeature;

[Route("/api/articles")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class ArticleController : ControllerBase
{
    private const string testUsername = "shamel";
    private readonly IArticleAppService _articleService;

    public ArticleController(IArticleAppService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> ListArticles(
        [FromQuery(Name = "author")] string? authorUsername,
        [FromQuery(Name = "tag")] string? tagName,
        [FromQuery(Name = "favorited")] string? favoritedUsername,
        [FromQuery(Name = "limit")] int limit = 20,
        [FromQuery(Name = "offset")] int offset = 0)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        var listArticlesRequestParams =
            CreateArticleRequestParams(authorUsername: authorUsername, tagName: tagName,
                favoritedUsername: favoritedUsername, limit: limit, offset: offset);
        var articles = await _articleService.ListArticlesAsync(authenticatedUsername, listArticlesRequestParams);
        var articleResponses = articles.ToList();
        return Ok(new { articles = articleResponses, count = articleResponses.Count() });
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

    [HttpGet("/feed")]
    public async Task<IActionResult> ListFeed(
        [FromQuery(Name = "limit")] int limit = 20,
        [FromQuery(Name = "offset")] int offset = 0)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        var listArticlesRequestParams =
            CreateArticleRequestParams(authenticatedUsername, limit: limit, offset: offset);
        var articles = (await _articleService.ListArticlesAsync(authenticatedUsername, listArticlesRequestParams)).ToList();
        return Ok(new { articles = articles, count = articles.Count() });
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, slug);
        return Ok(new { article });
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle([FromBody] ArticleRequest articleRequest)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        await _articleService.CreateArticle(articleRequest, authenticatedUsername);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, articleRequest.Slug);
        return Ok(new { article });
    }
    
    // todo authorize that user has the updated article
    [HttpPut("{slug}")]
    public async Task<IActionResult> UpdateArticle([FromRoute] string slug, [FromBody] ArticleUpdateRequest articleRequest)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        await _articleService.UpdateArticle(articleRequest, slug);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, articleRequest.Slug ?? slug);
        return Ok(new { article });
    }
    
    // todo authorize that user has the deleted article
    [HttpDelete("{slug}")]
    public async Task<IActionResult> DeleteArticle([FromRoute] string slug)
    {
        await _articleService.DeleteArticleAsync(slug);
        return NoContent();
    }
    
    // todo authenticate user
    [HttpPost("{slug}/favorite")]
    public async Task<IActionResult> FavoriteArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        await _articleService.FavoriteArticleAsync(slug, authenticatedUsername);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, slug);
        return Ok(new { article });
    }
    
    // todo authenticate user
    [HttpDelete("{slug}/favorite")]
    public async Task<IActionResult> UnfavoriteArticle([FromRoute] string slug)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        await _articleService.UnfavoriteArticleAsync(slug, authenticatedUsername);
        var article = await _articleService.GetArticleBySlug(authenticatedUsername, slug);
        return Ok(new { article });
    }

    
    
    
}