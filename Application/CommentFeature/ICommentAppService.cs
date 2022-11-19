namespace Application.CommentFeature;

public interface ICommentAppService
{
    Task<long> AddCommentAsync(CommentRequest commentRequest, string authorUsername, string slug);
    Task<CommentResponse> GetCommentAsync(long id);
    Task<IEnumerable<CommentResponse>> GetCommentsAsync(string FollowedByUser, string slug);
    Task DeleteCommentAsync(long id);
}