using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class SaleAppService(
    IUnitOfWork unitOfWork,
    ISaleService saleService,
    ISaleReceiptService saleReceiptService) : ISaleAppService
{
    public async Task AddSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var sale = request.MapToEntity();
        await saleService.CreateAsync(sale, cancellationToken);
        await unitOfWork.Sales.AddAsync(sale, cancellationToken);
        if (request.Receipts is not null)
        {
            var receipts = request.Receipts.MapToEntity();
            await saleReceiptService.AddRangeAsync(sale, receipts, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSaleAsync(Guid id, UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var sale = request.MapToEntity();
        await saleService.UpdateSale(id, sale, cancellationToken);
        unitOfWork.Sales.Update(sale);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task InvoiceSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException("Pedido não encontrado");
        await saleService.Invoice(saleDb, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Sales.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(GetSaleFilter filter,
        CancellationToken cancellationToken)
    {
        if (filter.Status == SaleStatus.All)
        {
            return (await unitOfWork.Sales.GetAllByExpressionAsync(x =>
                    x.CreateAt.Date >= filter.InitialDate.Date &&
                    x.CreateAt.Date <= filter.FinalDate.Date &&
                    (filter.ClientId == Guid.Empty || x.ClientId == filter.ClientId) &&
                    (filter.UserId == Guid.Empty || x.UserId == filter.UserId) &&
                    (filter.Document == 0 || x.Document == filter.Document),
                cancellationToken)).MapToResponse();
        }

        return (await unitOfWork.Sales.GetAllByExpressionAsync(x =>
                x.CreateAt.Date >= filter.InitialDate.Date &&
                x.CreateAt.Date <= filter.FinalDate.Date &&
                (filter.ClientId == Guid.Empty || x.ClientId == filter.ClientId) &&
                (filter.UserId == Guid.Empty || x.UserId == filter.UserId) &&
                (filter.Document == 0 || x.Document == filter.Document) &&
                (x.Status == filter.Status),
            cancellationToken)).MapToResponse();
    }

    public async Task<SaleResponse> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        return saleDb.MapToResponse();
    }

    public async Task DeleteSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        unitOfWork.Sales.Delete(saleDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task CancelSaleAsync(Guid id, SaleCancelReasonRequest request, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        await saleService.CancelSale(saleDb, request.Reason, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}