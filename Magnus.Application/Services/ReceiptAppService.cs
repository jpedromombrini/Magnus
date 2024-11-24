using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ReceiptAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IReceiptAppService
{
    public async Task AddReceiptAsync(CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.Receipts.AddAsync(mapper.Map<Receipt>(request), cancellationToken);
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
        unitOfWork.Receipts.UpdateAsync(receiptDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ReceiptResponse>> GetReceiptsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ReceiptResponse>>(await unitOfWork.Receipts.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<ReceiptResponse>> GetReceiptsByFilterAsync(Expression<Func<Receipt, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ReceiptResponse>>(
            await unitOfWork.Receipts.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<ReceiptResponse> GetReceiptByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<ReceiptResponse>(await unitOfWork.Receipts.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        var receiptDb = await unitOfWork.Receipts.GetByIdAsync(id, cancellationToken);
        if (receiptDb is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Receipts.DeleteAsync(receiptDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}