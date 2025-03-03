using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class CostCenterAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICostCenterAppService
{
    public async Task AddCostCenterAsync(CreateCostCenterRequest request, CancellationToken cancellationToken)
    {
        var costcenterDb =
            await unitOfWork.CostCenters.GetByExpressionAsync(x => x.Code == request.Code, cancellationToken);
        if (costcenterDb is not null)
            throw new ApplicationException("Já existe um centro de custo com esse código");
        costcenterDb = await unitOfWork.CostCenters.GetByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (costcenterDb is not null)
            throw new ApplicationException("Já existe um centro de custo com esse nome");
        await unitOfWork.CostCenters.AddAsync(mapper.Map<CostCenter>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCostCenterAsync(Guid id, UpdateCostCenterRequest request,
        CancellationToken cancellationToken)
    {
        var costCenter = await unitOfWork.CostCenters.GetByIdAsync(id, cancellationToken);
        if (costCenter is null)
            throw new EntityNotFoundException(id);
        costCenter.SetName(request.Name);
        costCenter.SetCode(request.Code);
        unitOfWork.CostCenters.Update(costCenter);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CostCenterResponse>> GetCostCentersAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<CostCenterResponse>>(await unitOfWork.CostCenters.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<CostCenterResponse>> GetCostCentersByFilterAsync(
        Expression<Func<CostCenter, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<CostCenterResponse>>(
            await unitOfWork.CostCenters.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<CostCenterResponse> GetCostCenterByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var costCenter = await unitOfWork.CostCenters.GetByIdAsync(id, cancellationToken);
        if (costCenter is null)
            throw new EntityNotFoundException(id);
        return mapper.Map<CostCenterResponse>(costCenter);
    }

    public async Task DeleteCostCenterAsync(Guid id, CancellationToken cancellationToken)
    {
        var costCenter = await unitOfWork.CostCenters.GetByIdAsync(id, cancellationToken);
        if (costCenter is null)
            throw new EntityNotFoundException(id);
        unitOfWork.CostCenters.Delete(costCenter);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}