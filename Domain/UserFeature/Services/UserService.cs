using System.Net;
using Domain.ArticleFeature.Services;
using Domain.CommentFeature.Services;
using Domain.Exceptions;
using Domain.Shared;
using Domain.UserFeature.Models;

namespace Domain.UserFeature.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleRepository _articleRepository;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IArticleRepository articleRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _commentRepository = commentRepository;
    }

    public async Task Create(User user)
    {
        if (await _userRepository.ExistsByUsername(user.Username))
            throw new ConduitException
                { Message = "Entered duplicated Username", StatusCode = HttpStatusCode.BadRequest };
        if (await _userRepository.ExistsByEmail(user.Email))
            throw new ConduitException
                { Message = "Entered duplicated Email", StatusCode = HttpStatusCode.BadRequest };
        _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await _userRepository.GetByUsername(username) ?? throw new ConduitException
            { Message = "No such username", StatusCode = HttpStatusCode.NotFound };
        return user;
    }

    public async Task<bool> IsFollowing(string follower, string followed)
    {
        return await _userRepository.IsFollowing(follower, followed);
    }

    public async Task FollowUser(string followerUsername, string followedUsername) //TODO should check follower?
    {
        if (!await _userRepository.ExistsByUsername(followedUsername))
            throw new ConduitException
                { Message = "No such username to follow", StatusCode = HttpStatusCode.NotFound };
        await _userRepository.FollowUser(followerUsername, followedUsername);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UnfollowUser(string followerUsername, string followedUsername)
    {
        if (!await _userRepository.ExistsByUsername(followedUsername))
            throw new ConduitException
                { Message = "No such username to Unfollow", StatusCode = HttpStatusCode.NotFound };
        await _userRepository.UnfollowUser(followerUsername, followedUsername);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email) ?? throw new ConduitException
            { Message = "No user with such email", StatusCode = HttpStatusCode.NotFound };
        return user;
    }

    public async Task<bool> UserHasArticleAsync(string username, string slug)
    {
        if (!await _articleRepository.ExistsBySlugAsync(slug))
        {
            throw new ConduitException
                { Message = "No such article", StatusCode = HttpStatusCode.NotFound };
        }
        if (!await _userRepository.ExistsByUsername(username))
            throw new ConduitException
                { Message = "No such username, make sure you're logged in", StatusCode = HttpStatusCode.NotFound };
        return await _userRepository.UserHasArticleAsync(username, slug);
    }

    public async Task<bool> UserHasCommentAsync(string username, long commentId)
    {
        if (!await _commentRepository.ExistsById(commentId)) 
            throw new ConduitException
                { Message = "No comment with such id", StatusCode = HttpStatusCode.NotFound };
        return await _userRepository.UserHasCommentAsync(username, commentId);
    }

    public async Task Update(string username, User updatedUser)
    {
        if (!await _userRepository.ExistsByUsername(username))
            throw new ConduitException
                { Message = "No such username, make sure you're logged in", StatusCode = HttpStatusCode.NotFound };
        var hasUniqueId = !await _userRepository.ExistsByUsername(updatedUser.Username) ||
                          username.Equals(updatedUser.Username);
        if (!hasUniqueId)
            throw new ConduitException
                { Message = "Entered duplicated username", StatusCode = HttpStatusCode.BadRequest };
        await _userRepository.Update(username, updatedUser);
        await _unitOfWork.SaveChangesAsync();
    }
}