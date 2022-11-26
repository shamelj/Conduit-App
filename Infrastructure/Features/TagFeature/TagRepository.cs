using Domain.Features.TagFeature.Models;
using Domain.Features.TagFeature.Services;
using Infrastructure.Features.ArticleFeature;
using Infrastructure.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.TagFeature;

public class TagRepository : ITagRepository
{
    private readonly DbSet<ArticleEntity> _articleDbSet;
    private readonly ConduitDbContext _context;
    private readonly DbSet<TagEntity> _dbSet;

    public TagRepository(ConduitDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TagEntity>();
        _articleDbSet = context.Set<ArticleEntity>();
    }

    public async Task Upsert(Tag tag)
    {
        if (!await _dbSet.AnyAsync(entity => entity.Name.Equals(tag.Name)))
            _dbSet.Add(tag.Adapt<TagEntity>());
    }

    public async Task<IEnumerable<Tag>> ListAsync()
    {
        return await _dbSet
            .Select(entity => entity.Adapt<Tag>())
            .ToListAsync();
    }

    public async Task<IEnumerable<Tag>> ListByArticleSlugAsync(string slug)
    {
        var tags = (await _articleDbSet.Include(entity => entity.Tags)
                .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug)))?
            .Tags
            .Select(entity => entity.Adapt<Tag>());
        return tags ?? Enumerable.Empty<Tag>();
    }
}