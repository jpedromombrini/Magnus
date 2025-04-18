using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IProductRepository : IRepository<Product>
{
    void DeleteBarsRange(Guid productId);
    void DeleteProductPriceTableRange(Guid productId);
}