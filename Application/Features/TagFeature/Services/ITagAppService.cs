namespace Application.Features.TagFeature.Services;

public interface ITagAppService
{
    Task<IEnumerable<string>> GetTagsAsync();
}