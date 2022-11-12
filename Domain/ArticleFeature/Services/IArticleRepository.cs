using Domain.ArticleFeature.Models;
using Domain.CommentFeature.Models;
using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.ArticleFeature.Services;

public interface IArticleRepository
{
    Task<IEnumerable<Tag>> ListTags(string slug);
    Task<IEnumerable<Comment>> ListComments(string slug);

    Task<bool> ExistsBySlug(string slug);
    Task Create(Article article);
    Task<Article?> GetBySlug(string slug);
    Task Update(string originalSlug, Article article);
    Task DeleteBySlug(string slug);
}