using AutoMapper;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class SaleReceiptAppService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : ISaleReceiptAppService
{
    public async Task<IEnumerable<SaleReceiptResponse>> GetSaleReceiptsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SaleReceiptResponse>>(
            await unitOfWork.SaleReceipts.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<SaleReceiptResponse>> GetSaleReceiptsByFilterAsync(GetSaleReceiptFilter filter,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SaleReceiptResponse>>(await unitOfWork.SaleReceipts.GetAllByExpressionAsync(
            x =>
                (filter.UserId == Guid.Empty || x.UserId == filter.UserId) &&
                (filter.ClientId == Guid.Empty || x.ClienteId == filter.ClientId) &&
                x.Installments.Any(y => y.DueDate >= filter.InitialDate && y.DueDate <= filter.FinalDate),
            cancellationToken));
    }

    public async Task<IEnumerable<SaleReceiptResponse>> ListSaleReceiptByIdAsync(Guid saleId,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.SaleReceipts.GetAllByExpressionAsync(x => x.SaleId == saleId, cancellationToken))
            .MapToResponse();
    }

    public async Task DeleteSaleReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        var saleReceiptDb = await unitOfWork.SaleReceipts.GetByIdAsync(id, cancellationToken);
        if (saleReceiptDb == null)
            throw new EntityNotFoundException("Recebimento não encontrado");
        unitOfWork.SaleReceipts.Delete(saleReceiptDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<SaleReceiptResponse> GetSaleReceiptByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<SaleReceiptResponse>(await unitOfWork.SaleReceipts.GetByIdAsync(id, cancellationToken));
    }
}