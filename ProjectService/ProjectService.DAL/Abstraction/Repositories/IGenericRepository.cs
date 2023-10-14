using ProjectService.DAL.Entities;
using System.Linq.Expressions;

namespace ProjectService.DAL.Abstraction.Repositories;

public interface IGenericRepository<TEntity> where TEntity: class
{
    IQueryable<TEntity> Query { get; }
    Task<TEntity> Add(TEntity entity, CancellationToken ct, ChangeLogEntity? changeLogEntity = null);
    Task<IEnumerable<TEntity>> Get(CancellationToken ct);
    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
    Task<TEntity?> GetById(Guid id, CancellationToken ct);
    Task<TEntity> Update(TEntity entity, CancellationToken ct, ChangeLogEntity? changeLogEntity = null);
    Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities, CancellationToken ct, List<ChangeLogEntity>? changeLogEntities = null);
    Task Delete(TEntity entity, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
}
