using Domain.CommentFeature.Models;
using Domain.CommentFeature.Services;
using Infrastructure.ArticleFeature;
using Infrastructure.Shared;
using Infrastructure.UserFeature;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CommentFeature;

public class CommentRepository : ICommentRepository
{
    private readonly DbSet<ArticleEntity> _articleDbSet;
    private readonly ConduitDbContext _context;
    private readonly DbSet<CommentEntity> _dbSet;
    private readonly DbSet<UserEntity> _userDbSet;

    public CommentRepository(ConduitDbContext context)
    {
        _context = context;
        _userDbSet = context.Set<UserEntity>();
        _articleDbSet = context.Set<ArticleEntity>();
        _dbSet = context.Set<CommentEntity>();
    }

    public void CreateAsync(Comment comment)
    {
        var commentEntity = ToCommentEntity(comment);
        _dbSet.Add(commentEntity);
    }

    public async Task DeleteByIdAsync(long id)
    {
        var comment = await _dbSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        _context.Entry(comment).State = EntityState.Deleted;
    }

    private CommentEntity ToCommentEntity(Comment comment)
    {
        var commentEntity = comment.Adapt<CommentEntity>();
        var author = _userDbSet.Single(user => user.Username.Equals(comment.Username));
        var article = _articleDbSet.Single(article => article.Slug.Equals(comment.ArticleSlug));
        commentEntity.Article = article;
        commentEntity.Author = author;
        commentEntity.CreatedAt = DateTime.Now;
        return commentEntity;
    }

    public async Task<IEnumerable<Comment>> ListCommentsAsync(string slug)
    {
        var comments = (await _articleDbSet.Include(entity => entity.Comments)
                .SingleOrDefaultAsync(entity => entity.Slug.Equals(slug)))?
            .Comments
            .Select(entity => entity.Adapt<Comment>());
        return comments ?? Enumerable.Empty<Comment>();
    }
}