using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public interface ITagService
{
    Task Upsert(Tag tag);
    Task<IEnumerable<Tag>> ListAsync();
}