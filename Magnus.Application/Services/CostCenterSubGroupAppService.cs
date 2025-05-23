using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class CostCenterSubGroupAppService(
    IUnitOfWork unitOfWork) : ICostCenterSubGroupAppService
{
    public async Task AddCostCenterSubGroupAsync(CreateCostCenterSubGroupRequest request,
        CancellationToken cancellationToken)
    {
        var costCenterSubGroup =
            await unitOfWork.CostCenterSubGroups.GetByExpressionAsync(x => x.Code == request.Code, cancellationToken);
        if (costCenterSubGroup is not null)
            throw new ApplicationException("Já existe um subgrupo de centro de custo com esse código");
        costCenterSubGroup = await unitOfWork.CostCenterSubGroups.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (costCenterSubGroup is not null)
            throw new ApplicationException("Já existe um subgrupo de centro de custo com esse nome");
        await unitOfWork.CostCenterSubGroups.AddAsync(request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCostCenterSubGroupAsync(Guid id, UpdateCostCenterSubGroupRequest request,
        CancellationToken cancellationToken)
    {
        var costCenterSubGroup = await unitOfWork.CostCenterSubGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterSubGroup is null)
            throw new EntityNotFoundException(id);
        costCenterSubGroup.SetName(request.Name);
        costCenterSubGroup.SetCode(request.Code);
        unitOfWork.CostCenterSubGroups.Update(costCenterSubGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CostCenterSubGroupResponse>> GetCostCenterSubGroupsAsync(
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.CostCenterSubGroups.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<CostCenterSubGroupResponse>> GetCostCenterSubGroupsByFilterAsync(
        Expression<Func<CostCenterSubGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return (await unitOfWork.CostCenterSubGroups.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<CostCenterSubGroupResponse> GetCostCenterSubGroupByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var costCenterSubGroup = await unitOfWork.CostCenterSubGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterSubGroup is null)
            throw new EntityNotFoundException(id);
        return costCenterSubGroup.MapToResponse();
    }

    public async Task DeleteCostCenterSubGroupAsync(Guid id, CancellationToken cancellationToken)
    {
        var costCenterSubGroup = await unitOfWork.CostCenterSubGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterSubGroup is null)
            throw new EntityNotFoundException(id);
        unitOfWork.CostCenterSubGroups.Delete(costCenterSubGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}