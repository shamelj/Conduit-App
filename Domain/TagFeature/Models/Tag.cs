using Domain.Shared;

namespace Domain.TagFeature.Models;

public class Tag : IBaseModel<string>
{
    public string Name { get; set; }

    public string GetId()
    {
        return Name;
    }
}