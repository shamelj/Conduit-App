// using Domain.Shared;
// using Mapster;
// using Microsoft.EntityFrameworkCore;
//
// namespace Infrastructure.Shared;
//
// public abstract class BaseRepository<TModel, TEntity, TId> : IRepository<TModel, TId>
//     where TModel : class, IBaseModel<TId>
//     where TEntity : class, IEntity<TId>, new()
// {
//     private readonly ConduitDbContext _context;
//     private readonly DbSet<TEntity> _dbSet;
//
//     public BaseRepository(ConduitDbContext context)
//     {
//         _context = context;
//         _dbSet = context.Set<TEntity>();
//     }
//
//     
//         public async Task<TModel?> FindById(TId id)
//     {
//         var entity = await _dbSet.SingleOrDefaultAsync(entity => entity.GetDomainId().Equals(id));
//         return entity?.Adapt<TModel>();
//     }
//
//     public async Task Update(TModel model)
//     {
//         var entityPresistanceId = await _dbSet
//             .SingleOrDefaultAsync(entity => entity.GetDomainId().Equals(model.GetId()));
//         var updatedEntity = model.
//         if (entity != null) _dbSet.Update(entity);
//     }
//
//     public void Create(TModel model)
//     {
//         _dbSet.Add(model.Adapt<TEntity>());
//     }
//
//     public void DeleteById(TId id)
//     {
//         _dbSet.Entry(entityToDelete).State = EntityState.Deleted;
//     }
// }