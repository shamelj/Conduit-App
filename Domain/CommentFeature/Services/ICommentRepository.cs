using Domain.CommentFeature.Models;
using Domain.Shared;

namespace Domain.CommentFeature.Services;

public interface ICommentRepository
{
    void Create(Comment comment);
    Task DeleteById(long id);
}