using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ICostCenterService
{
    Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}