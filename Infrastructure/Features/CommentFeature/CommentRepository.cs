﻿using Domain.Features.CommentFeature.Models;
using Domain.Features.CommentFeature.Services;
using Infrastructure.Features.ArticleFeature;
using Infrastructure.Features.UserFeature;
using Infrastructure.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.CommentFeature;

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

    public async Task<long> CreateAsync(Comment comment)
    {
        var commentEntity = await ToCommentEntityAsync(comment);
        var dateNow = DateTime.Now;
        commentEntity.CreatedAt = dateNow;
        commentEntity.UpdatedAt = dateNow;
        _dbSet.Add(commentEntity);
        await _context.SaveChangesAsync();
        return commentEntity.Id;
    }

    public void DeleteByIdAsync(long id)
    {
        var comment = new CommentEntity { Id = id };
        _context.Entry(comment).State = EntityState.Deleted;
    }


    public async Task<IEnumerable<Comment>> ListCommentsAsync(string slug)
    {
        var comments = await _dbSet
            .Include(comment => comment.Article)
            .Include(comment => comment.Author)
            .Where(comment => comment.Article.Slug.Equals(slug))
            .OrderByDescending(comment => comment.CreatedAt)
            .Select(commentEntity => commentEntity.Adapt<Comment>())
            .ToListAsync();
        return comments;
    }

    public async Task<Comment?> GetCommentAsync(long id)
    {
        var commentEntity = await _dbSet
            .Include(comment => comment.Article)
            .Include(comment => comment.Author)
            .SingleOrDefaultAsync(comment => comment.Id.Equals(id));
        return commentEntity?.Adapt<Comment>();
    }

    public async Task<bool> ExistsById(long commentId)
    {
        return await _dbSet.AnyAsync(comment => comment.Id.Equals(commentId));
    }

    private async Task<CommentEntity> ToCommentEntityAsync(Comment comment)
    {
        var commentEntity = comment.Adapt<CommentEntity>();
        var author = await _userDbSet.SingleAsync(user => user.Username.Equals(comment.Username));
        var article = await _articleDbSet.SingleAsync(article => article.Slug.Equals(comment.ArticleSlug));
        commentEntity.Article = article;
        commentEntity.Author = author;
        return commentEntity;
    }
}