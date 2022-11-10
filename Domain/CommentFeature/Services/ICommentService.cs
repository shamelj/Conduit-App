using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentService : IService<Comment, long>
{
}