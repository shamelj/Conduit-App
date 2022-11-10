using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public class CommentService : BaseService<Comment, long>, ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository) : base(commentRepository)
    {
        _commentRepository = commentRepository;
    }
}