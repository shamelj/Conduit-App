namespace Domain.Shared;

public interface IRepository<TModel, in TId> where TModel : IBaseModel<TId>
{
    TModel FindById(TId id);
    TModel Update(TModel model);
    TModel Create(TModel model);
    bool ExistsById(TId id);
    void DeleteById(TId id);
}