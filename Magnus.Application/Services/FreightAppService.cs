using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class FreightAppService(IUnitOfWork unitOfWork) : IFreightAppService
{
    public async Task AddFreightAsync(CreateFreightRequest request, CancellationToken cancellationToken)
    {
        var freight = request.MapToEntity();
        var freightDb = await unitOfWork.Freights.GetByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (freightDb != null)
            throw new BusinessRuleException("já existe um tipo de frete com esse nome");
        await unitOfWork.Freights.AddAsync(freight, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateFreightAsync(Guid id, UpdateFreightRequest request, CancellationToken cancellationToken)
    {
        var freightDb = await unitOfWork.Freights.GetByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (freightDb != null)
            throw new BusinessRuleException("já existe um tipo de frete com esse nome");
        freightDb = await unitOfWork.Freights.GetByIdAsync(id, cancellationToken);
        if (freightDb == null)
            throw new EntityNotFoundException("nenhum frete encontrado com esse id");
        freightDb.SetName(request.Name);
        unitOfWork.Freights.Update(freightDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<FreightResponse>> GetFreightsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Freights.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<FreightResponse>> GetFreightsByFilterAsync(Expression<Func<Freight, bool>> predicate, CancellationToken cancellationToken)
    {
        return (await unitOfWork.Freights.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<FreightResponse> GetFreightByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var freight = await unitOfWork.Freights.GetByIdAsync(id, cancellationToken);
        if(freight == null)
            throw new EntityNotFoundException("nenhum frete encontrado com esse id");
        return freight.MapResponse();
    }

    public async Task DeleteFreightAsync(Guid id, CancellationToken cancellationToken)
    {
        var freight = await unitOfWork.Freights.GetByIdAsync(id, cancellationToken);
        if(freight == null)
            throw new EntityNotFoundException("nenhum frete encontrado com esse id");
        unitOfWork.Freights.Delete(freight);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}