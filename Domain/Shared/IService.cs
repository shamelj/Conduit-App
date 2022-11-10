namespace Domain.Shared;

public interface IService<TModel, in TId> where TModel : IBaseModel<TId>
{
    TModel GetById(TId id);
    TModel Update(TId id, TModel model);
    TModel Create(TModel model);
    bool ExistsById(TId id);
    void DeleteById(TId id);
}