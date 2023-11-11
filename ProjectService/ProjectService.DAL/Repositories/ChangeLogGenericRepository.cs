using Microsoft.EntityFrameworkCore;
using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Contexts;
using ProjectService.DAL.Entities;
using ProjectService.DAL.Entities.Base;
using TinyHelpers.Extensions;

namespace ProjectService.DAL.Repositories;

internal class ChangeLogGenericRepository<TEntity> : IChangeLogGenericRepository<TEntity>
    where TEntity : EntityWithId
{
    protected readonly DatabaseContext Context;
    protected readonly DbSet<TEntity> DbSet;
    protected readonly DbSet<ChangeLogEntity> ChangeLogDbSet;
    public IQueryable<TEntity> Query => DbSet.AsQueryable();

    public ChangeLogGenericRepository(DatabaseContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = Context.Set<TEntity>();
        ChangeLogDbSet = Context.Set<ChangeLogEntity>();
    }

    public async Task<TEntity> Update(TEntity entity, ChangeLogEntity changeLog, CancellationToken ct)
    {
        changeLog.ChangeAt = DateTime.UtcNow;
        await ChangeLogDbSet.AddAsync(changeLog, ct);

        DbSet.Update(entity);
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities, IEnumerable<ChangeLogEntity> changeLogs, CancellationToken ct)
    {
        changeLogs = changeLogs.ToList();
        changeLogs.ForEach(x => x.ChangeAt = DateTime.UtcNow);
        ChangeLogDbSet.AddRange(changeLogs);

        DbSet.UpdateRange(entities);
        await Context.SaveChangesAsync(ct);
        return entities;
    }

    public virtual async Task Delete(TEntity entity, ChangeLogEntity changeLog, CancellationToken ct)
    {
        changeLog.ChangeAt = DateTime.UtcNow;
        await ChangeLogDbSet.AddAsync(changeLog, ct);

        DbSet.Remove(entity);
        await Context.SaveChangesAsync(ct);
    }

    public virtual async Task Delete(Guid id, ChangeLogEntity changeLog, CancellationToken ct)
    {
        changeLog.ChangeAt = DateTime.UtcNow;
        await ChangeLogDbSet.AddAsync(changeLog, ct);

        var entity = await DbSet.FindAsync(new object[] { id }, ct);
        if (entity is not null)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync(ct);
        }
    }

    public virtual async Task<TEntity> Add(TEntity entity, ChangeLogEntity changeLog, CancellationToken ct)
    {
        changeLog.CreatedAt = DateTime.UtcNow;
        changeLog.ChangeAt = DateTime.UtcNow;
        await ChangeLogDbSet.AddAsync(changeLog, ct);

        entity.CreatedAt = DateTime.UtcNow;
        await DbSet.AddAsync(entity, ct);
        await Context.SaveChangesAsync(ct);
        return entity;
    }
}
