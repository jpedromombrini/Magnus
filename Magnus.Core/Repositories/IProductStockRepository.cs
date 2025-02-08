using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IProductStockRepository : IRepository<ProductStock>
{
    Task AddRangeAsync(List<ProductStock> productStocks, CancellationToken cancellationToken);
}