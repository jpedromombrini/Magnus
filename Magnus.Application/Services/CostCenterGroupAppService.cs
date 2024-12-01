using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class CostCenterGroupAppService(
    IUnitOfWork unitOfWork, IMapper mapper) : ICostCenterGroupAppService
{
    public async Task AddCostCenterGroupAsync(CreateCostCenterGroupRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.CostCenterGroups.AddAsync(mapper.Map<CostCenterGroup>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCostCenterGroupAsync(Guid id, UpdateCostCenterGroupRequest request, CancellationToken cancellationToken)
    {
        var costCenterGroup = await unitOfWork.CostCenterGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterGroup is null)
            throw new EntityNotFoundException(id);
        costCenterGroup.SetName(request.Name);
        unitOfWork.CostCenterGroups.Update(costCenterGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<CostCenterGroupResponse>>(await unitOfWork.CostCenterGroups.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsByFilterAsync(Expression<Func<CostCenterGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<CostCenterGroupResponse>>(
            await unitOfWork.CostCenterGroups.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<CostCenterGroupResponse> GetCostCenterGroupByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var costCenterGroup = await unitOfWork.CostCenterGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterGroup is null)
            throw new EntityNotFoundException(id);
        return mapper.Map<CostCenterGroupResponse>(costCenterGroup);
    }

    public async Task DeleteCostCenterGroupAsync(Guid id, CancellationToken cancellationToken)
    {
        var costCenterGroup = await unitOfWork.CostCenterGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterGroup is null)
            throw new EntityNotFoundException(id);
        unitOfWork.CostCenterGroups.Delete(costCenterGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}