using System.Linq.Expressions;
using Mapster;
using ProjectService.BLL.Abstraction.Services;
using ProjectService.DAL.Abstraction.Repositories;

namespace ProjectService.BLL.Services;

public class GenericService<TModel, TEntity> : IGenericService<TModel>
        where TEntity : class
        where TModel : class
{
    protected readonly IGenericRepository<TEntity> Repository;

    public GenericService(IGenericRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public virtual async Task<TModel> Add(TModel model, CancellationToken ct)
    {
        return (await Repository.Add(model.Adapt<TEntity>(), ct)).Adapt<TModel>();
    }

    public Task Delete(TModel model, CancellationToken ct)
    {
        return Repository.Delete(model.Adapt<TEntity>(), ct);
    }

    public virtual Task Delete(Guid id, CancellationToken ct)
    {
        return Repository.Delete(id, ct);
    }

    public virtual async Task<TModel> Update(TModel model, CancellationToken ct)
    {
        return (await Repository.Update(model.Adapt<TEntity>(), ct)).Adapt<TModel>();
    }

    public async Task<IEnumerable<TModel>> Get(Expression<Func<TModel, bool>> predicate, CancellationToken ct)
    {
        var result = (await Repository.Get(predicate.Adapt<Expression<Func<TEntity, bool>>>(), ct))
                .Adapt<IEnumerable<TModel>>();

        return result;
    }

    public virtual async Task<IEnumerable<TModel>> Get(CancellationToken ct)
    {
        var result = (await Repository.Get(ct)).Adapt<IEnumerable<TModel>>();

        return result;
    }

    public virtual async Task<TModel> GetById(Guid id, CancellationToken ct)
    {
        var res = (await Repository.GetById(id, ct)).Adapt<TModel>();

        return res;
    }
}
