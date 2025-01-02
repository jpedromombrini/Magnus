using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class LaboratoryAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ILaboratoryAppService
{
    public async Task AddLaboratoryAsync(CreateLaboratoryRequest request, CancellationToken cancellationToken)
    {
        var laboratoryDb = await unitOfWork.Laboratories.GetAllByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (laboratoryDb is not null)
            throw new ApplicationException("Já existe um laboratório com esse nome");
        await unitOfWork.Laboratories.AddAsync(mapper.Map<Laboratory>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateLaboratoryAsync(Guid id, UpdateLaboratoryRequest request,
        CancellationToken cancellationToken)
    {
        var laboratory = await unitOfWork.Laboratories.GetByIdAsync(id, cancellationToken);
        if (laboratory is null)
            throw new EntityNotFoundException(id);
        laboratory.SetName(request.Name);
        unitOfWork.Laboratories.Update(laboratory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<LaboratoryResponse>> GetLaboratoriesAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<LaboratoryResponse>>(
            await unitOfWork.Laboratories.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<LaboratoryResponse>> GetLaboratoriesByFilterAsync(
        Expression<Func<Laboratory, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<LaboratoryResponse>>(
            await unitOfWork.Laboratories.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<LaboratoryResponse> GetLaboratoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var laboratory = await unitOfWork.Laboratories.GetByIdAsync(id, cancellationToken);
        if (laboratory is null)
            throw new EntityNotFoundException(id);
        return mapper.Map<LaboratoryResponse>(laboratory);
    }

    public async Task DeleteLaboratoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var laboratory = await unitOfWork.Laboratories.GetByIdAsync(id, cancellationToken);
        if (laboratory is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Laboratories.Delete(laboratory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}