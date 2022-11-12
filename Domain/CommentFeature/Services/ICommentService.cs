using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentService
{
    Task Create(Comment comment);
    Task DeleteById(long id);

}