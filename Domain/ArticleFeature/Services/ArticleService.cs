using System.Net;
using Domain.ArticleFeature.Models;
using Domain.Exceptions;
using Domain.Shared;

namespace Domain.ArticleFeature.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
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
        var hasUniqueId = !await _articleRepository.ExistsBySlugAsync(article.Slug) ||
                          originalSlug.Equals(article.Slug);
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
}