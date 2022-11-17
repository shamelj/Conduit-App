using Domain.ArticleFeature.Models;
using Domain.ArticleFeature.Services;
using Domain.CommentFeature.Models;
using Domain.TagFeature.Models;
using Infrastructure.Shared;
using Infrastructure.TagFeature;
using Infrastructure.UserFeature;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ArticleFeature;

public class ArticleRepository : IArticleRepository
{
    private readonly ConduitDbContext _context;
    private readonly DbSet<ArticleEntity> _dbSet;
    private readonly DbSet<UserEntity> _userDbSet;


    public ArticleRepository(ConduitDbContext context)
    {
        _context = context;
        _dbSet = context.Set<ArticleEntity>();
        _userDbSet = context.Set<UserEntity>();
    }
    
    public async Task<bool> ExistsBySlugAsync(string slug)
    {
        return await _dbSet.AnyAsync(entity => entity.Slug.Equals(slug));
    }

    public async Task CreateAsync(Article article)
    {
        var articleEntity = await ToArticleEntityAsync(article);
        articleEntity.CreatedAt = DateTime.Now;
        _dbSet.Add(articleEntity);
    }

    private async Task<ArticleEntity> ToArticleEntityAsync(Article article)
    {
        var author = await _userDbSet.SingleAsync(entity => entity.Username.Equals(article.Username));
        var articleEntity = article.Adapt<ArticleEntity>();
        articleEntity.Author = author;
        articleEntity.Tags = article.TagList?
            .Select(tagName => new TagEntity { Name = tagName }) ?? Enumerable.Empty<TagEntity>();
        return articleEntity;
    }

    public async Task<Article?> GetBySlugAsync(string slug)
    {
        return (await _dbSet
                .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug)))?
            .Adapt<Article>();
    }

    public async Task UpdateAsync(string originalSlug, Article article)
    {
        var articleEntity = await ToArticleEntityAsync(article);
        var originalArticleEntity = _dbSet.SingleOrDefault(entity => entity.Slug.Equals(originalSlug));
        var resultEntity = articleEntity.Adapt(originalArticleEntity);
        resultEntity.UpdatedAt = DateTime.Now;
        _dbSet.Update(resultEntity);
    }

    public async Task DeleteBySlugAsync(string slug)
    {
        var articleEntity = await _dbSet.SingleOrDefaultAsync(entity => entity.Slug.Equals(slug));
        _context.Entry(articleEntity).State = EntityState.Deleted;
    }
}