using ProjectService.DAL.Entities.Base;
using ProjectService.DAL.Entities;

namespace ProjectService.DAL.Abstraction.Repositories;

public interface IChangeLogGenericRepository<TEntity>
    where TEntity : EntityWithId
{
    Task<TEntity> Update(TEntity entity, ChangeLogEntity changeLog, CancellationToken ct);
    Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities, IEnumerable<ChangeLogEntity> changeLogs, CancellationToken ct);
    Task Delete(TEntity entity, ChangeLogEntity changeLog, CancellationToken ct);
    Task Delete(Guid id, ChangeLogEntity changeLog, CancellationToken ct);
    Task<TEntity> Add(TEntity entity, ChangeLogEntity changeLog, CancellationToken ct);
}
