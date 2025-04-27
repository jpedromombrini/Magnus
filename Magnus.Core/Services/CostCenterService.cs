using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class CostCenterService(IUnitOfWork unitOfWork) : ICostCenterService
{
    public async Task<CostCenter?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return await unitOfWork.CostCenters.GetByExpressionAsync(x => x.Code == code, cancellationToken);
    }
}