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

    public async Task<IEnumerable<Tag>> ListTags(string slug)
    {
        var tags = (await _dbSet.Include(entity => entity.Tags)
                .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug)))?
            .Tags
            .Select(entity => entity.Adapt<Tag>());
        return tags ?? Enumerable.Empty<Tag>();
    }

    public async Task<IEnumerable<Comment>> ListComments(string slug)
    {
        var comments = (await _dbSet.Include(entity => entity.Comments)
                .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug)))?
            .Comments
            .Select(entity => entity.Adapt<Comment>());
        return comments ?? Enumerable.Empty<Comment>();
    }

    public async Task<bool> ExistsBySlug(string slug)
    {
        return await _dbSet.AnyAsync(entity => entity.Slug.Equals(slug));
    }

    public async Task Create(Article article)
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

    public async Task<Article?> GetBySlug(string slug)
    {
        return (await _dbSet
                .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug)))?
            .Adapt<Article>();
    }

    public async Task Update(string originalSlug, Article article)
    {
        var articleEntity = await ToArticleEntityAsync(article);
        var originalArticleEntity = _dbSet.SingleOrDefault(entity => entity.Slug.Equals(originalSlug));
        var resultEntity = articleEntity.Adapt(originalArticleEntity);
        resultEntity.UpdatedAt = DateTime.Now;
        _dbSet.Update(resultEntity);
    }

    public async Task DeleteBySlug(string slug)
    {
        var articleEntity = await _dbSet.SingleOrDefaultAsync(entity => entity.Slug.Equals(slug));
        _context.Entry(articleEntity).State = EntityState.Deleted;
    }
}