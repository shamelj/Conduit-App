using Domain.Features.ArticleFeature.Models;
using Domain.Features.UserFeature.Services;
using Domain.Shared;

namespace Domain.Features.ArticleFeature.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserRepository _userRepository;

    private readonly IArticleValidator _validator;

    public ArticleService(IArticleRepository articleRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IArticleValidator validator)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task CreateAsync(Article article)
    {
        await _validator.SlugMustBeUniqueAsync(article.Slug);
        await _articleRepository.CreateAsync(article);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Article> GetBySlugAsync(string slug)
    {
        await _validator.ArticleMustExistBySlugAsync(slug);
        var article = await _articleRepository.GetBySlugAsync(slug);
        return article;
    }

    public async Task UpdateAsync(string originalSlug, Article article)
    {
        await _validator.ArticleMustExistBySlugAsync(originalSlug);
        if (!originalSlug.Equals(article.Slug))
            await _validator.SlugMustBeUniqueAsync(article.Slug);
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
        await _validator.ArticleMustExistBySlugAsync(slug);
        return await _articleRepository.FavoritedByUser(slug, username);
    }

    public async Task<int> CountFavorites(string slug)
    {
        return await _articleRepository.CountFavorites(slug);
    }

    public async Task FavoriteArticleAsync(string slug, string followingUsername)
    {
        await _validator.ArticleMustExistBySlugAsync(slug);
        await _validator.UserMustExistAsync(followingUsername);
        if (await _articleRepository.FavoritedByUser(slug, followingUsername))
            return;
        await _articleRepository.FavoriteArticleAsync(slug, followingUsername);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UnfavoriteArticleAsync(string slug, string followingUsername)
    {
        await _validator.ArticleMustExistBySlugAsync(slug);
        await _validator.UserMustExistAsync(followingUsername);
        await _articleRepository.UnfavoriteArticleAsync(slug, followingUsername);
        await _unitOfWork.SaveChangesAsync();
    }
}