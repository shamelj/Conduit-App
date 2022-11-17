using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentRepository
{
    void CreateAsync(Comment comment);
    Task DeleteByIdAsync(long id);
    Task<IEnumerable<Comment>> ListCommentsAsync(string slug);
}