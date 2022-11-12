using Domain.CommentFeature.Models;
using Domain.CommentFeature.Services;
using Infrastructure.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CommentFeature;

public class CommentRepository : ICommentRepository
{
    private readonly ConduitDbContext _context;
    private readonly DbSet<CommentEntity> _dbSet;

    public CommentRepository(ConduitDbContext context)
    {
        _context = context;
        _dbSet = context.Set<CommentEntity>();
    }
    public void Create(Comment comment)
    {
        var commentEntity = comment.Adapt<CommentEntity>();
        commentEntity.CreatedAt = DateTime.Now;
        _dbSet.Add(commentEntity);
    }

    public async Task DeleteById(long id)
    {
        var comment = await _dbSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        _context.Entry(comment).State = EntityState.Deleted;
    }
}