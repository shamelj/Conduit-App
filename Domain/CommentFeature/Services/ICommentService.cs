using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentService
{
    Task CreateAsync(Comment comment);
    Task DeleteByIdAsync(long id);

}