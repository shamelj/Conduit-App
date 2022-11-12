// namespace Domain.Shared;
//
// public interface IRepository<TModel, in TId> where TModel : IBaseModel<TId>
// {
//     Task<TModel?> FindById(TId id);
//     Task Update(TModel model);
//     void Create(TModel model);
//     Task<bool> ExistsById(TId id);
//     void DeleteById(TId id);
// }