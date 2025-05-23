using System.Linq.Expressions;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface ICostCenterGroupAppService
{
    Task AddCostCenterGroupAsync(CreateCostCenterGroupRequest request, CancellationToken cancellationToken);
    Task UpdateCostCenterGroupAsync(Guid id, UpdateCostCenterGroupRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsByFilterAsync(GetCostCenterFilter filter, CancellationToken cancellationToken);
    Task<CostCenterGroupResponse> GetCostCenterGroupByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteCostCenterGroupAsync(Guid id, CancellationToken cancellationToken);
}