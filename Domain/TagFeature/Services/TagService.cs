using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public class TagService : BaseService<Tag, string>, ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository) : base(tagRepository)
    {
        _tagRepository = tagRepository;
    }
}