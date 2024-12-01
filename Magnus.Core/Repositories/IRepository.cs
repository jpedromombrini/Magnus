using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IRepository<T> where T : EntityBase
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity);
    Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}