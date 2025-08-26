using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class SaleAppService(
    IUnitOfWork unitOfWork,
    ISaleService saleService,
    IAppConfigurationService configurationService,
    IClientService clientService) : ISaleAppService
{
    public async Task AddSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var sale = request.MapToEntity();
        await saleService.CreateAsync(sale, cancellationToken);
        await unitOfWork.Sales.AddAsync(sale, cancellationToken);
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
        var client = await clientService.ValidateClientAsync(saleDb.ClientId, cancellationToken);
        await saleService.Invoice(saleDb, client, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Sales.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(GetSaleFilter filter,
        CancellationToken cancellationToken)
    {
        if (filter.UserType == UserType.Admin)
            return (await unitOfWork.Sales.GetAllByExpressionAsync(x =>
                    x.CreateAt.Date >= filter.InitialDate.Date &&
                    x.CreateAt.Date <= filter.FinalDate.Date &&
                    (filter.ClientId == null || x.ClientId == filter.ClientId) &&
                    (filter.UserId == null || x.UserId == filter.UserId) &&
                    (filter.Document == 0 || x.Document == filter.Document) &&
                    (filter.Status == SaleStatus.All || x.Status == filter.Status),
                cancellationToken)).MapToResponse();

        return (await unitOfWork.Sales.GetAllByExpressionAsync(x =>
                x.CreateAt.Date >= filter.InitialDate.Date &&
                x.CreateAt.Date <= filter.FinalDate.Date &&
                (filter.ClientId == null || x.ClientId == filter.ClientId) &&
                x.UserId == filter.UserId &&
                (filter.Document == 0 || x.Document == filter.Document) &&
                (filter.Status == SaleStatus.All || x.Status == filter.Status),
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

    public async Task GenerateAccontsReceivable(CancellationToken cancellationToken)
    {
        var sales = await unitOfWork.Sales.GetAllByExpressionAsync(x => x.Status == SaleStatus.Invoiced,
            cancellationToken);
        var configuration = await configurationService.GetAppConfigurationAsync(cancellationToken);
        foreach (var sale in sales)
        {
            var crs = new List<AccountsReceivable>();
            var client = await clientService.ValidateClientAsync(sale.ClientId, cancellationToken);
            foreach (var receipt in sale.Receipts)
            foreach (var installment in receipt.Installments)
            {
                var crExists =
                    await unitOfWork.AccountsReceivables.GetByExpressionAsync(
                        x => x.SaleReceiptInstallmentId == installment.Id, cancellationToken);
                if (crExists is not null && (crExists.ReceiptId is null || crExists.ReceiptId == Guid.Empty))
                {
                    var receiptnew = await unitOfWork.Receipts.GetByIdAsync(receipt.ReceiptId, cancellationToken);
                    if (receiptnew is not null)
                    {
                        crExists.SetReceiptId(receipt.ReceiptId);
                        crExists.SetReceipt(receiptnew);
                    }

                    unitOfWork.AccountsReceivables.Update(crExists);
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}