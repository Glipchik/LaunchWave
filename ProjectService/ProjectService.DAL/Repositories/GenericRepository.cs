using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Contexts;
using ProjectService.DAL.Entities;
using ProjectService.DAL.Entities.Base;

namespace ProjectService.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityWithId
{
    protected readonly DatabaseContext Context;
    protected readonly DbSet<TEntity> DbSet;
    protected readonly IChangeLogGenericRepository<TEntity> ChangeLogRepository;
    public IQueryable<TEntity> Query => DbSet.AsQueryable();

    public GenericRepository(DatabaseContext context, IChangeLogGenericRepository<TEntity> changeLoggedRepository)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ChangeLogRepository = changeLoggedRepository;
        DbSet = Context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> Get(CancellationToken ct)
    {
        return await DbSet.AsNoTracking().ToListAsync(ct);
    }

    public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync(ct);
    }

    public virtual async Task<TEntity?> GetById(Guid id, CancellationToken ct)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<TEntity> Update(TEntity entity, CancellationToken ct, ChangeLogEntity? changeLogEntity = null)
    {
        if (changeLogEntity is not null)
        {
            return await ChangeLogRepository.Update(entity, changeLogEntity, ct);
        }
        entity.UpdatedAt = DateTime.UtcNow;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities, CancellationToken ct, List<ChangeLogEntity>? changeLogEntities = null)
    {
        if (changeLogEntities is not null)
        {
            return await ChangeLogRepository.UpdateRange(entities, changeLogEntities, ct);
        }

        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
        }
        DbSet.UpdateRange(entities);
        await Context.SaveChangesAsync(ct);
        return entities;
    }

    public virtual Task Delete(TEntity entity, CancellationToken ct)
    {
        DbSet.Remove(entity);
        return Context.SaveChangesAsync(ct);
    }
    public virtual async Task Delete(Guid id, CancellationToken ct)
    {
        var entity = await DbSet.FindAsync(new object[] { id }, ct);
        if (entity is not null)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync(ct);
        }
    }

    public virtual async Task<TEntity> Add(TEntity entity, CancellationToken ct, ChangeLogEntity? changeLogEntity = null)
    {
        if (changeLogEntity is not null)
        {
            return await ChangeLogRepository.Add(entity, changeLogEntity, ct);
        }

        entity.CreatedAt = DateTime.UtcNow;
        await DbSet.AddAsync(entity, ct);
        await Context.SaveChangesAsync(ct);
        return entity;
    }
}
