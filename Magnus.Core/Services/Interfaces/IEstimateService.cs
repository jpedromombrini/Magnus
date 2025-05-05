using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IEstimateService
{
    Task CreateEstimateAsync(Estimate estimate, CancellationToken cancellationToken);
    Task UpdateEstimateAsync(Guid id, Estimate estimate, CancellationToken cancellationToken);
    Task CreateSaleAsync(Guid id, CancellationToken cancellationToken);
}