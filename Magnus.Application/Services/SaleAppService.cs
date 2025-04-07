using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class SaleAppService(
    IUnitOfWork unitOfWork,
    ISaleService saleService,
    ISaleReceiptService saleReceiptService,
    IMapper mapper) : ISaleAppService
{
    public async Task AddSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var sale = mapper.Map<Sale>(request);
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
        var receipts = request.Receipts.MapToEntity();
        await saleService.UpdateSale(saleDb, clientDb, userDb, items, receipts, request.Value,
            request.FinantialDiscount, cancellationToken);
        unitOfWork.Sales.Update(saleDb);
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