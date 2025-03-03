using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface ICostCenterAppService
{
    Task AddCostCenterAsync(CreateCostCenterRequest request, CancellationToken cancellationToken);
    Task UpdateCostCenterAsync(Guid id, UpdateCostCenterRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<CostCenterResponse>> GetCostCentersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CostCenterResponse>> GetCostCentersByFilterAsync(Expression<Func<CostCenter, bool>> predicate, CancellationToken cancellationToken);
    Task<CostCenterResponse> GetCostCenterByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteCostCenterAsync(Guid id, CancellationToken cancellationToken);
}