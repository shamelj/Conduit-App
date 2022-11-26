using Domain.Features.TagFeature.Models;

namespace Domain.Features.TagFeature.Services;

public interface ITagRepository
{
    Task Upsert(Tag tag);
    Task<IEnumerable<Tag>> ListAsync();
    Task<IEnumerable<Tag>> ListByArticleSlugAsync(string slug);
}