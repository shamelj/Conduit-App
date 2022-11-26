using Application.Features.CommentFeature.Models;

namespace Application.Features.CommentFeature.Services;

public interface ICommentAppService
{
    Task<long> AddCommentAsync(CommentRequest commentRequest, string authorUsername, string slug);
    Task<CommentResponse> GetCommentAsync(long id, string requestingUser);
    Task<IEnumerable<CommentResponse>> GetCommentsAsync(string followedByUser, string slug);
    Task DeleteCommentAsync(long id);
}