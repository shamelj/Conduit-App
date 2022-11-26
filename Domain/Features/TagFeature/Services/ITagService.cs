using Domain.Features.TagFeature.Models;

namespace Domain.Features.TagFeature.Services;

public interface ITagService
{
    Task Upsert(Tag tag);
    Task<IEnumerable<Tag>> ListAsync();
}