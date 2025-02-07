﻿using Domain.Features.ArticleFeature.Models;
using Domain.Features.ArticleFeature.Services;
using Infrastructure.Features.TagFeature;
using Infrastructure.Features.UserFeature;
using Infrastructure.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.ArticleFeature;

public class ArticleRepository : IArticleRepository
{
    private readonly ConduitDbContext _context;
    private readonly DbSet<ArticleEntity> _dbSet;
    private readonly DbSet<UserFavouriteArticleEntity> _favouritedArticlesDbSet;
    private readonly DbSet<TagEntity> _tagDbSet;
    private readonly DbSet<UserEntity> _userDbSet;


    public ArticleRepository(ConduitDbContext context)
    {
        _context = context;
        _dbSet = context.Set<ArticleEntity>();
        _userDbSet = context.Set<UserEntity>();
        _favouritedArticlesDbSet = context.Set<UserFavouriteArticleEntity>();
        _tagDbSet = context.Set<TagEntity>();
    }

    public async Task<bool> ExistsBySlugAsync(string slug)
    {
        return await _dbSet.AnyAsync(entity => entity.Slug.Equals(slug));
    }

    public async Task CreateAsync(Article article)
    {
        var articleEntity = await ToArticleEntityAsync(article);
        var dateNow = DateTime.Now;
        articleEntity.CreatedAt = dateNow;
        articleEntity.UpdatedAt = dateNow;
        _dbSet.Add(articleEntity);
    }

    public async Task<Article?> GetBySlugAsync(string slug)
    {
        var articleEntity = await _dbSet
            .Include(article => article.Author)
            .Include(article => article.Tags)
            .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug));
        return articleEntity is null ? null : ToArticle(articleEntity);
    }

    public async Task UpdateAsync(string originalSlug, Article article)
    {
        var articleEntity = await ToArticleEntityAsync(article);
        var originalArticleEntity = _dbSet
            .Include(art => art.Author)
            .Include(art => art.Tags)
            .SingleOrDefault(entity => entity.Slug.Equals(originalSlug));
        var resultEntity = articleEntity.Adapt(originalArticleEntity);
        resultEntity.UpdatedAt = DateTime.Now;
        if (article.TagList is not null)
            resultEntity.Tags = articleEntity.Tags;
        _dbSet.Update(resultEntity);
    }

    public async Task DeleteBySlugAsync(string slug)
    {
        var articleEntity = await _dbSet.SingleOrDefaultAsync(entity => entity.Slug.Equals(slug));

        if (articleEntity is not null)
            _dbSet.Remove(articleEntity);
    }

    public async Task<IEnumerable<Article>>
        ListArticlesAsync(ListArticlesRequestParams requestParams)
    {
        var query = _dbSet
            .Include(article => article.Author)
            .Include(article => article.Tags)
            .Include(article => article.UserFavouriteArticles)
            .ThenInclude(userArticle => userArticle.User)
            .AsQueryable();
        if (!requestParams.AuthorUsername.IsNullOrEmpty())
            query = query.FilterByAuthorUsername(requestParams.AuthorUsername);
        if (!requestParams.FavoritedUsername.IsNullOrEmpty())
            query = query.FilterByFavoritedUsername(requestParams.FavoritedUsername);
        if (!requestParams.TagName.IsNullOrEmpty())
            query = query.FilterByTagName(requestParams.TagName);
        if (!requestParams.FeedForUser.IsNullOrEmpty())
            query = query.FilterByFollowedAuthors(requestParams.FeedForUser);

        query = query
            .OrderByDescending(article => article.CreatedAt)
            .Skip(requestParams.Offset)
            .Take(requestParams.Limit);
        var articlesEntities = await query.ToListAsync();
        var articles = articlesEntities.Select(ToArticle);
        return articles;
    }


    public async Task<bool> FavoritedByUser(string slug, string username)
    {
        var favoritedArticles = _favouritedArticlesDbSet
            .Include(favArticle => favArticle.User)
            .Include(favArticle => favArticle.Article);
        return await favoritedArticles.AnyAsync(favArticle =>
            favArticle.Article.Slug.Equals(slug) && favArticle.User.Username.Equals(username));
    }

    public async Task<int> CountFavorites(string slug)
    {
        return await _favouritedArticlesDbSet.CountAsync(favArticle => favArticle.Article.Slug.Equals(slug));
    }

    public async Task FavoriteArticleAsync(string slug, string followingUsername)
    {
        var user = await _userDbSet.SingleOrDefaultAsync(user => user.Username.Equals(followingUsername));
        var article = await _dbSet.SingleOrDefaultAsync(article => article.Slug.Equals(slug));
        var userFavArticle = new UserFavouriteArticleEntity
        {
            User = user,
            Article = article
        };
        _favouritedArticlesDbSet.Add(userFavArticle);
    }

    public async Task UnfavoriteArticleAsync(string slug, string followingUsername)
    {
        var userFavArticle = await _favouritedArticlesDbSet
            .Include(userArticle => userArticle.Article)
            .Include(userArticle => userArticle.User)
            .Where(userArticle => userArticle.Article.Slug.Equals(slug))
            .Where(userArticle => userArticle.User.Username.Equals(followingUsername))
            .SingleOrDefaultAsync();

        if (userFavArticle != null) _favouritedArticlesDbSet.Remove(userFavArticle);
    }

    private async Task<ArticleEntity> ToArticleEntityAsync(Article article)
    {
        var author = await _userDbSet.SingleOrDefaultAsync(entity => entity.Username.Equals(article.AuthorUsername));
        var articleEntity = article.Adapt<ArticleEntity>();
        articleEntity.Author = author;
        if (article.TagList != null) articleEntity.Tags = await ToTagEntityListAsync(article.TagList);
        return articleEntity;
    }

    private async Task<IEnumerable<TagEntity>> ToTagEntityListAsync(IEnumerable<string> tagList)
    {
        var tagEntities = new List<TagEntity>();
        foreach (var tagName in tagList)
        {
            var tagEntity = await _tagDbSet.SingleOrDefaultAsync(tag => tag.Name.Equals(tagName));
            if (tagEntity is null) tagEntity = new TagEntity { Name = tagName };
            tagEntities.Add(tagEntity);
        }

        return tagEntities;
    }

    private Article ToArticle(ArticleEntity articleEntitiy)
    {
        var article = articleEntitiy.Adapt<Article>();
        article.TagList = articleEntitiy.Tags.Select(tagEntity => tagEntity.Name);
        return article;
    }
}