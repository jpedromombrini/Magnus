using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface ICostCenterSubGroupAppService
{
    Task AddCostCenterSubGroupAsync(CreateCostCenterSubGroupRequest request, CancellationToken cancellationToken);
    Task UpdateCostCenterSubGroupAsync(Guid id, UpdateCostCenterSubGroupRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<CostCenterSubGroupResponse>> GetCostCenterSubGroupsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CostCenterSubGroupResponse>> GetCostCenterSubGroupsByFilterAsync(Expression<Func<CostCenterSubGroup, bool>> predicate, CancellationToken cancellationToken);
    Task<CostCenterSubGroupResponse> GetCostCenterSubGroupByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteCostCenterSubGroupAsync(Guid id, CancellationToken cancellationToken);
}