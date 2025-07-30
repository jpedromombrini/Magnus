using Magnus.Core.Entities;
using Magnus.Core.Enumerators;

namespace Magnus.Core.Repositories;

public interface ICostCenterRepository : IRepository<CostCenter>
{
    Task<IEnumerable<CostCenter>> GetAllByTypeGroupAsync(CostcenterGroupType costcenterGroupType,
        CancellationToken cancellationToken);
}