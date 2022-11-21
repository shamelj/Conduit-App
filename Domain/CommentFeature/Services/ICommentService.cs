using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentService
{
    Task<long> CreateAsync(Comment comment);
    Task DeleteByIdAsync(long id);

    Task<Comment> GetCommentAsync(long id);
    Task<IEnumerable<Comment>> GetCommentsAsync(string slug);
}