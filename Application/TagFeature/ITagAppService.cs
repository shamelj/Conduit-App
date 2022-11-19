namespace Application.TagFeature;

public interface ITagAppService
{
    Task<IEnumerable<string>> GetTagsAsync();
}