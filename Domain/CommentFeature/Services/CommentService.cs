using System.Net;
using Domain.ArticleFeature.Services;
using Domain.CommentFeature.Models;
using Domain.Exceptions;
using Domain.Shared;
using Domain.UserFeature.Services;

namespace Domain.CommentFeature.Services;

public class CommentService : ICommentService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(ICommentRepository commentRepository,
        IUserRepository userRepository,
        IArticleRepository articleRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> CreateAsync(Comment comment)
    {
        if (comment.Id != null)
            throw new ConduitException
                { Message = "Id must be empty, it's autogenerated", StatusCode = HttpStatusCode.BadRequest };
        if (!await _userRepository.ExistsByUsername(comment.Username))
            throw new ConduitException
                { Message = "No such username, make sure you're logged in", StatusCode = HttpStatusCode.BadRequest };
        if (!await _articleRepository.ExistsBySlugAsync(comment.ArticleSlug))
            throw new ConduitException
                { Message = "No article with such Slug", StatusCode = HttpStatusCode.BadRequest };
        var id = await _commentRepository.CreateAsync(comment);
        await _unitOfWork.SaveChangesAsync();
        return id;
    }

    public async Task DeleteByIdAsync(long id)
    {
        _commentRepository.DeleteByIdAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Comment> GetComment(long id)
    {
        var comment = await _commentRepository.GetCommentAsync(id) ?? throw new ConduitException
            { Message = "No comment with such id", StatusCode = HttpStatusCode.NotFound };
        return comment;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(string slug)
    {
        if (!await _articleRepository.ExistsBySlugAsync(slug))
            throw new ConduitException
                { Message = "No article with such Slug", StatusCode = HttpStatusCode.BadRequest };
        return await _commentRepository.ListCommentsAsync(slug);

    }
}