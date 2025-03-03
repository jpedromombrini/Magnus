using AutoMapper;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class SaleAppService(
    ISaleService saleService,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ISaleAppService
{
    public async Task AddSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByIdAsync(request.ClientId, cancellationToken);
        if (clientDb == null)
            throw new EntityNotFoundException("Cliente não encontrado");
        var userDb = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (userDb == null)
            throw new EntityNotFoundException("usuário não encontrado");
        var sale = mapper.Map<Sale>(request);
        saleService.CreateSale(sale, clientDb, userDb);
        await unitOfWork.Sales.AddAsync(sale, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSaleAsync(Guid id, UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        
        var clientDb = await unitOfWork.Clients.GetByIdAsync(request.ClientId, cancellationToken);
        if (clientDb == null)
            throw new EntityNotFoundException("Cliente não encontrado");
        
        var userDb = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (userDb == null)
            throw new EntityNotFoundException("usuário não encontrado");
        var items = mapper.Map<IEnumerable<SaleItem>>(request.Items);
        var receipts = mapper.Map<IEnumerable<SaleReceipt>>(request.Receipts);
        saleService.UpdateSale(saleDb, clientDb, userDb, items, receipts, request.Value, request.FinantialDiscount);
        unitOfWork.Sales.Update(saleDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Invoice(Guid id, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        if(saleDb.Receipts.Count == 0)
            throw new BusinessRuleException("Recebimento não encontrado");
        await saleService.Invoice(saleDb, cancellationToken);
        unitOfWork.Sales.Update(saleDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SaleResponse>>(await unitOfWork.Sales.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(GetSaleFilter filter,
        CancellationToken cancellationToken)
    {
        if (filter.Status == SaleStatus.All)
        {
            return mapper.Map<IEnumerable<SaleResponse>>(await unitOfWork.Sales.GetAllByExpressionAsync(x =>
                    x.CreateAt.Date >= filter.InitialDate.Date &&
                    x.CreateAt.Date <= filter.FinalDate.Date &&
                    (filter.ClientId == Guid.Empty || x.ClientId == filter.ClientId) &&
                    (filter.UserId == Guid.Empty || x.UserId == filter.UserId) &&
                    (filter.Document == 0 || x.Document == filter.Document),
                cancellationToken));
        }

        return mapper.Map<IEnumerable<SaleResponse>>(await unitOfWork.Sales.GetAllByExpressionAsync(x =>
                x.CreateAt.Date >= filter.InitialDate.Date &&
                x.CreateAt.Date <= filter.FinalDate.Date &&
                (filter.ClientId == Guid.Empty || x.ClientId == filter.ClientId) &&
                (filter.UserId == Guid.Empty || x.UserId == filter.UserId) &&
                (filter.Document == 0 || x.Document == filter.Document) &&
                (x.Status == filter.Status),
            cancellationToken));
    }

    private async Task FillReceipts(IEnumerable<SaleReceipt> receipts, CancellationToken cancellationToken)
    {
        foreach (var receipt in receipts)
        {
            var receiptDb = await unitOfWork.Receipts.GetByIdAsync(receipt.ReceiptId, cancellationToken);
            if (receiptDb == null)
                throw new EntityNotFoundException("Tipo recebimento não encontrado");
            receipt.SetReceipt(receiptDb); 
        }
    }

    public async Task<SaleResponse> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<SaleResponse>(await unitOfWork.Sales.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        unitOfWork.Sales.Delete(saleDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}