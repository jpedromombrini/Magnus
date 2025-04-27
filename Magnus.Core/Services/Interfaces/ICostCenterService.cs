using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ICostCenterService
{
    Task<CostCenter?> GetByCodeAsync(string code, CancellationToken cancellationToken);
}