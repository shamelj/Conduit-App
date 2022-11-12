using System.Net;
using Domain.ArticleFeature.Models;
using Domain.Exceptions;

namespace Domain.ArticleFeature.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task Create(Article article)
    {
        if (await _articleRepository.ExistsBySlug(article.Slug))
            throw new ConduitException
                { Message = "Entered duplicated title/slug", StatusCode = HttpStatusCode.BadRequest };
        _articleRepository.Create(article);
    }

    public async Task<Article> GetBySlug(string slug)
    {
        var article = await _articleRepository.GetBySlug(slug) ?? throw new ConduitException
            { Message = "No such slug", StatusCode = HttpStatusCode.NotFound };
        return article;
    }

    public async Task Update(string originalSlug, Article article)
    {
        if (!await _articleRepository.ExistsBySlug(originalSlug))
            throw new ConduitException
                { Message = "No such slug to update", StatusCode = HttpStatusCode.NotFound };
        var hasUniqueId = !await _articleRepository.ExistsBySlug(article.Slug) ||
                          originalSlug.Equals(article.Slug);
        if (!hasUniqueId)
            throw new ConduitException
                { Message = "Entered duplicated title/slug", StatusCode = HttpStatusCode.BadRequest };
        await _articleRepository.Update(originalSlug, article);
    }

    public async Task DeleteBySlug(string slug)
    {
        await _articleRepository.DeleteBySlug(slug);
    }
}