using Domain.TagFeature.Services;

namespace Application.TagFeature;

public class TagAppService : ITagAppService
{
    private readonly ITagService _tagService;

    public TagAppService(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<IEnumerable<string>> GetTagsAsync()
    {
        var tags = await _tagService.ListAsync();
        return tags
            .Select(tag => tag.Name)
            .ToList();
    }
}