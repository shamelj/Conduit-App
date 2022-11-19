using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public interface ITagRepository
{
    Task Upsert(Tag tag);
    Task<IEnumerable<Tag>> ListAsync();
    Task<IEnumerable<Tag>> ListByArticleSlugAsync(string slug);
}