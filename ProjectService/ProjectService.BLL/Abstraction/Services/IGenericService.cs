using System.Linq.Expressions;

namespace ProjectService.BLL.Abstraction.Services;

public interface IGenericService<TModel> where TModel: class 
{
    Task<TModel> Add(TModel model, CancellationToken ct);
    Task<IEnumerable<TModel>> Get(CancellationToken ct);
    Task<IEnumerable<TModel>> Get(Expression<Func<TModel, bool>> predicate, CancellationToken ct);
    Task<TModel> GetById(Guid id, CancellationToken ct);
    Task<TModel> Update(TModel model, CancellationToken ct);
    Task Delete(TModel model, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
}
