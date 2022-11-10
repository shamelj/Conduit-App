using Domain.ArticleFeature.Models;
using Domain.Shared;

namespace Domain.ArticleFeature.Services;

public interface IArticleService : IService<Article, string>
{
}