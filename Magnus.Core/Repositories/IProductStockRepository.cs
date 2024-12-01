using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IProductStockRepository : IRepository<ProductStock>
{
    Task<ProductStock?> GetByExpressionAsync(Expression<Func<ProductStock, bool>> predicate, CancellationToken cancellationToken);
    Task AddRangeAsync(List<ProductStock> productStocks, CancellationToken cancellationToken);
}