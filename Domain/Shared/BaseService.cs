using System.Net;
using Domain.Exceptions;

namespace Domain.Shared;

public class BaseService<TModel, TId> : IService<TModel, TId>
    where TModel : IBaseModel<TId>
{
    private readonly IRepository<TModel, TId> _mainRepository;

    public BaseService(IRepository<TModel, TId> mainRepository)
    {
        _mainRepository = mainRepository;
    }

    public TModel GetById(TId id)
    {
        var model = _mainRepository.FindById(id) ?? throw new ConduitException
            { Message = "No such Id", StatusCode = HttpStatusCode.NotFound };
        return model;
    }

    public TModel Update(TId id, TModel model)
    {
        ValidateBeforeUpdate(id, model);
        return _mainRepository.Update(model);
    }

    public TModel Create(TModel model)
    {
        ValidateBeforeCreate(model);
        return _mainRepository.Create(model);
    }

    public bool ExistsById(TId id)
    {
        return _mainRepository.ExistsById(id);
    }

    public void DeleteById(TId id)
    {
        ValidateBeforeDelete(id);
        _mainRepository.DeleteById(id);
    }

    protected void ValidateBeforeUpdate(TId id, TModel model)
    {
        if (!_mainRepository.ExistsById(id))
            throw new ConduitException
                { Message = "No such Id", StatusCode = HttpStatusCode.NotFound };
        var hasUniqueId = !_mainRepository.ExistsById(model.GetId()) || id.Equals(model.GetId());
        if (!hasUniqueId)
            throw new ConduitException
                { Message = "Entered duplicated Id", StatusCode = HttpStatusCode.BadRequest };
    }

    protected void ValidateBeforeCreate(TModel model)
    {
        if (_mainRepository.ExistsById(model.GetId()))
            throw new ConduitException
                { Message = "Entered duplicated Id", StatusCode = HttpStatusCode.NotFound };
    }

    protected void ValidateBeforeDelete(TId id)
    {
    }
}