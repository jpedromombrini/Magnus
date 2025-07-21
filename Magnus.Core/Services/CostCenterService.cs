using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class CostCenterService(IUnitOfWork unitOfWork) : ICostCenterService
{
    public async Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await unitOfWork.CostCenters.GetByIdAsync(id, cancellationToken);
    }
}