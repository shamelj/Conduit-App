using Domain.Shared;
using Domain.TagFeature.Models;

namespace Domain.TagFeature.Services;

public interface ITagRepository : IRepository<Tag, string>
{
}