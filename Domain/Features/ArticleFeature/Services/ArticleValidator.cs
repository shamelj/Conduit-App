using System.Net;
using Domain.Features.UserFeature.Services;
using Domain.Shared.Exceptions;

namespace Domain.Features.ArticleFeature.Services;

public class ArticleValidator : IArticleValidator
{
    private readonly IArticleRepository _articleRepository;

    private readonly IUserRepository _userRepository;

    public ArticleValidator(IArticleRepository articleRepository, IUserRepository userRepository)
    {
        _articleRepository = articleRepository;
        _userRepository = userRepository;
    }

    public async Task SlugMustBeUniqueAsync(string slug)
    {
        if (await _articleRepository.ExistsBySlugAsync(slug))
            throw new ConduitException
                { Message = "Entered duplicated title/slug", StatusCode = HttpStatusCode.BadRequest };
    }

    public async Task ArticleMustExistBySlugAsync(string slug)
    {
        if (!await _articleRepository.ExistsBySlugAsync(slug))
            throw new ConduitException
                { Message = "No such article", StatusCode = HttpStatusCode.NotFound };
    }

    public async Task UserMustExistAsync(string username)
    {
        if (!await _userRepository.ExistsByUsername(username))
            throw new ConduitException
                { Message = "No such username", StatusCode = HttpStatusCode.NotFound };
    }
}