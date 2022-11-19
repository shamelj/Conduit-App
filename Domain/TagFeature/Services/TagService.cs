using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private IUnitOfWork _unitOfWork;

    public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Upsert(Tag tag)
    {
        await _tagRepository.Upsert(tag);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<IEnumerable<Tag>> ListAsync()
    {
        return await _tagRepository.ListAsync();
    }
}