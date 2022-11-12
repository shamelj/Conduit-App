using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public void Upsert(Tag tag)
    {
        _tagRepository.Upsert(tag);
    }

    public async Task<IEnumerable<Tag>> List()
    {
        return await _tagRepository.List();
    }
}