using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentRepository
{
    Task<long> CreateAsync(Comment comment);
    void DeleteByIdAsync(long id);
    Task<IEnumerable<Comment>> ListCommentsAsync(string slug);
    Task<Comment?> GetCommentAsync(long id);
}