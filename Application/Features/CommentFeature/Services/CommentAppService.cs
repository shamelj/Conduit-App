using Application.Features.CommentFeature.Models;
using Application.Features.UserFeature.Services;
using Domain.Features.CommentFeature.Models;
using Domain.Features.CommentFeature.Services;
using Mapster;

namespace Application.Features.CommentFeature.Services;

public class CommentAppService : ICommentAppService
{
    private readonly ICommentService _commentService;

    private readonly IUserAppService _userAppService;


    public CommentAppService(ICommentService commentService, IUserAppService userAppService)
    {
        _commentService = commentService;
        _userAppService = userAppService;
    }

    public async Task<long> AddCommentAsync(CommentRequest commentRequest, string authorUsername, string slug)
    {
        var comment = ToComment(commentRequest, authorUsername, slug);
        var id = await _commentService.CreateAsync(comment);
        return id;
    }

    public async Task<CommentResponse> GetCommentAsync(long id, string requestingUser)
    {
        var comment = await _commentService.GetCommentAsync(id);
        var commentResponse = await ToCommentResponse(comment, requestingUser);
        return commentResponse;
    }

    public async Task<IEnumerable<CommentResponse>> GetCommentsAsync(string followedByUser, string slug)
    {
        var comments = await _commentService.GetCommentsAsync(slug);
        var commentsResponse = comments
            .Select(comment => ToCommentResponse(comment, followedByUser).Result);
        return commentsResponse;
    }

    public async Task DeleteCommentAsync(long id)
    {
        await _commentService.DeleteByIdAsync(id);
    }

    private async Task<CommentResponse> ToCommentResponse(Comment comment, string requestingUser)
    {
        var commentResponse = comment.Adapt<CommentResponse>();
        commentResponse.Author = await _userAppService.GetProfileByUsernameAsync(comment.Username, requestingUser);
        return commentResponse;
    }

    private static Comment ToComment(CommentRequest commentRequest, string authorUsername, string slug)
    {
        var comment = commentRequest.Adapt<Comment>();
        comment.Username = authorUsername;
        comment.ArticleSlug = slug;
        return comment;
    }
}