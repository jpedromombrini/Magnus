using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class CostCenterGroupAppService(
    IUnitOfWork unitOfWork) : ICostCenterGroupAppService
{
    public async Task AddCostCenterGroupAsync(CreateCostCenterGroupRequest request, CancellationToken cancellationToken)
    {
        var costCenterGroupDb =
            await unitOfWork.CostCenterGroups.GetByExpressionAsync(
                x => x.Code == request.Code && x.CostcenterGroupType == request.CostcenterGroupType, cancellationToken);
        if (costCenterGroupDb is not null)
            throw new ApplicationException("Já existe um grupo de centro de custo com esse código");
        costCenterGroupDb = await unitOfWork.CostCenterGroups.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower() && x.CostcenterGroupType == request.CostcenterGroupType,
            cancellationToken);
        if (costCenterGroupDb is not null)
            throw new ApplicationException("Já existe um grupo de centro de custo com esse nome");
        await unitOfWork.CostCenterGroups.AddAsync(request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCostCenterGroupAsync(Guid id, UpdateCostCenterGroupRequest request,
        CancellationToken cancellationToken)
    {
        var costCenterGroup = await unitOfWork.CostCenterGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterGroup is null)
            throw new EntityNotFoundException(id);
        costCenterGroup.SetName(request.Name);
        costCenterGroup.SetCostCenterGroupType(request.CostcenterGroupType);
        unitOfWork.CostCenterGroups.Update(costCenterGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsAsync(
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.CostCenterGroups.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsByFilterAsync(
        GetCostCenterFilter filter, CancellationToken cancellationToken)
    {
        return (await unitOfWork.CostCenterGroups.GetAllByExpressionAsync(
            x =>
                (string.IsNullOrEmpty(filter.Name) || x.Name.ToLower() == filter.Name.ToLower()) &&
                (filter.CostCenterGroupType == null || x.CostcenterGroupType == filter.CostCenterGroupType),
            cancellationToken)).MapToResponse();
    }

    public async Task<CostCenterGroupResponse> GetCostCenterGroupByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var costCenterGroup = await unitOfWork.CostCenterGroups.GetByIdAsync(id, cancellationToken);
        if (costCenterGroup is null)
            throw new EntityNotFoundException(id);
        return costCenterGroup.MapToResponse();
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