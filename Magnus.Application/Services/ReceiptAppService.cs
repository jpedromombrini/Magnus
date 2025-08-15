using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ReceiptAppService(
    IUnitOfWork unitOfWork) : IReceiptAppService
{
    public async Task AddReceiptAsync(CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        var receiptDb = await unitOfWork.Receipts.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (receiptDb is not null)
            throw new BusinessRuleException("Já existe um Recebimento com esse nome");
        await unitOfWork.Receipts.AddAsync(request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateReceiptAsync(Guid id, UpdateReceiptRequest request, CancellationToken cancellationToken)
    {
        var receiptDb = await unitOfWork.Receipts.GetByIdAsync(id, cancellationToken);
        if (receiptDb is null)
            throw new EntityNotFoundException(id);
        receiptDb.SetName(request.Name);
        receiptDb.SetIncrease(request.Increase);
        receiptDb.SetInIstallments(request.InInstallments);
        unitOfWork.Receipts.Update(receiptDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ReceiptResponse>> GetReceiptsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Receipts.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<ReceiptResponse>> GetReceiptsByFilterAsync(Expression<Func<Receipt, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.Receipts.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<ReceiptResponse> GetReceiptByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var receiptDb = await unitOfWork.Receipts.GetByIdAsync(id, cancellationToken);
        if (receiptDb is null)
            throw new EntityNotFoundException(id);
        return receiptDb.MapToResponse();
    }

    public async Task DeleteReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        var receiptDb = await unitOfWork.Receipts.GetByIdAsync(id, cancellationToken);
        if (receiptDb is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Receipts.Delete(receiptDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}