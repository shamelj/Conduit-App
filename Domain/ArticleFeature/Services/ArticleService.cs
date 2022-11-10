using Domain.ArticleFeature.Models;
using Domain.Shared;

namespace Domain.ArticleFeature.Services;

public class ArticleService : BaseService<Article, string>, IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository) : base(articleRepository)
    {
        _articleRepository = articleRepository;
    }
}