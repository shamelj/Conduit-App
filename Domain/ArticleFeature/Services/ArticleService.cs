using System.Net;
using Domain.ArticleFeature.Models;
using Domain.Exceptions;
using Domain.Shared;
using Domain.UserFeature.Services;

namespace Domain.ArticleFeature.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task CreateAsync(Article article)
    {
        if (await _articleRepository.ExistsBySlugAsync(article.Slug))
            throw new ConduitException
                { Message = "Entered duplicated title/slug", StatusCode = HttpStatusCode.BadRequest };
        await _articleRepository.CreateAsync(article);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Article> GetBySlugAsync(string slug)
    {
        var article = await _articleRepository.GetBySlugAsync(slug) ?? throw new ConduitException
            { Message = "No such slug", StatusCode = HttpStatusCode.NotFound };
        return article;
    }

    public async Task UpdateAsync(string originalSlug, Article article)
    {
        if (!await _articleRepository.ExistsBySlugAsync(originalSlug))
            throw new ConduitException
                { Message = "No such slug to update", StatusCode = HttpStatusCode.NotFound };
        var hasUniqueId = originalSlug.Equals(article.Slug) || !await _articleRepository.ExistsBySlugAsync(article.Slug);
        if (!hasUniqueId)
            throw new ConduitException
                { Message = "Entered duplicated title/slug", StatusCode = HttpStatusCode.BadRequest };
        await _articleRepository.UpdateAsync(originalSlug, article);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteBySlugAsync(string slug)
    {
        await _articleRepository.DeleteBySlugAsync(slug);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Article>> ListArticlesAsync(ListArticlesRequestParams requestParams)
    {
        return await _articleRepository.ListArticlesAsync(requestParams);
    }

    public async Task<bool> FavoritedByUser(string slug, string username)
    {
        if (!await _articleRepository.ExistsBySlugAsync(slug))
            throw new ConduitException
                { Message = "No such slug", StatusCode = HttpStatusCode.NotFound };
        return await _articleRepository.FavoritedByUser(slug, username);
    }

    public async Task<int> CountFavorites(string slug)
    {
        return await _articleRepository.CountFavorites(slug);
    }

    public async Task FavoriteArticleAsync(string slug, string followingUsername)
    {
        if (!await _articleRepository.ExistsBySlugAsync(slug))
            throw new ConduitException
                { Message = "No such slug", StatusCode = HttpStatusCode.NotFound };
        if (!await _userRepository.ExistsByUsername(followingUsername))
            throw new ConduitException
                { Message = "No such username", StatusCode = HttpStatusCode.NotFound };
        if (await _articleRepository.FavoritedByUser(slug, followingUsername))
            return;
        await _articleRepository.FavoriteArticleAsync(slug, followingUsername);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UnfavoriteArticleAsync(string slug, string followingUsername)
    {
        if (!await _articleRepository.ExistsBySlugAsync(slug))
            throw new ConduitException
                { Message = "No such slug", StatusCode = HttpStatusCode.NotFound };
        if (!await _userRepository.ExistsByUsername(followingUsername))
            throw new ConduitException
                { Message = "No such username", StatusCode = HttpStatusCode.NotFound };
        await _articleRepository.UnfavoriteArticleAsync(slug, followingUsername);
        await _unitOfWork.SaveChangesAsync();
    }
}