using Domain.ArticleFeature.Models;
using Domain.Shared;

namespace Domain.ArticleFeature.Services;

public interface IArticleService
{
    Task Create(Article article);
    Task<Article> GetBySlug(string slug);
    Task Update(string originalSlug, Article article);
    Task DeleteBySlug(string slug);
}