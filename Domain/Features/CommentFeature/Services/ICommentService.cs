using Domain.Features.CommentFeature.Models;

namespace Domain.Features.CommentFeature.Services;

public interface ICommentService
{
    Task<long> CreateAsync(Comment comment);
    Task DeleteByIdAsync(long id);

    Task<Comment> GetCommentAsync(long id);
    Task<IEnumerable<Comment>> GetCommentsAsync(string slug);
}