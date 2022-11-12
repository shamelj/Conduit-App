using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public interface ITagService
{
    void Upsert(Tag tag);
    Task<IEnumerable<Tag>> List();
}