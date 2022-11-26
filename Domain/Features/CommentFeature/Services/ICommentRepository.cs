using Domain.Features.CommentFeature.Models;

namespace Domain.Features.CommentFeature.Services;

public interface ICommentRepository
{
    Task<long> CreateAsync(Comment comment);
    void DeleteByIdAsync(long id);
    Task<IEnumerable<Comment>> ListCommentsAsync(string slug);
    Task<Comment?> GetCommentAsync(long id);
    Task<bool> ExistsById(long commentId);
}