using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IEstimateRepository : IRepository<Estimate>
{
    void DeleteItensRange(IEnumerable<EstimateItem> items);
}