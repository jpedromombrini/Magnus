using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class SaleAppService(
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
        sale.SetCreateAt(DateTime.Now);
        sale.SetClientName(clientDb.Name);
        sale.SetStatus(SaleStatus.Open);
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

        foreach (var itemRequest in request.Items)
        {
            var existingItem = saleDb.Items.SingleOrDefault(item => item.ProductId == itemRequest.ProductId);
            if (existingItem != null)
            {
                existingItem.SetAmount(itemRequest.Amount);
                existingItem.SetDiscount(itemRequest.Discount);
                existingItem.SetValue(itemRequest.Value);
                existingItem.SetValidity(itemRequest.Validity);
                existingItem.SetTotalPrice(itemRequest.TotalPrice);
            }
            else
            {
                var newItem = mapper.Map<SaleItem>(itemRequest);
                saleDb.AddItem(newItem);
            }
        }
        
        foreach (var saleReceiptRequest in request.Receipts)
        {
            var existingReceipt = saleDb.Receipts.SingleOrDefault(r => r.ReceiptId == saleReceiptRequest.ReceiptId);
            if (existingReceipt != null)
            {
                foreach (var installment in saleReceiptRequest.Installments)
                {
                    existingReceipt.AddInstallment(mapper.Map<SaleReceiptInstallment>(installment));
                }
            }
            else
            {
                var saleReceipt = new SaleReceipt(saleDb, mapper.Map<Receipt>(saleReceiptRequest));
                foreach (var installment in saleReceiptRequest.Installments)
                {
                    saleReceipt.AddInstallment(mapper.Map<SaleReceiptInstallment>(installment));
                }
                saleDb.AddReceipt(saleReceipt);
            }
        }
        var itemsToRemove = saleDb.Items
            .Where(item => request.Items.All(requestItem => requestItem.ProductId != item.ProductId)).ToList();
        unitOfWork.Sales.DeleteItensRange(itemsToRemove);

        saleDb.SetClientId(request.ClientId);
        saleDb.SetClientName(clientDb.Name);
        saleDb.SetValue(request.Value);
        saleDb.SetFinantialDiscount(request.FinantialDiscount);
        unitOfWork.Sales.Update(saleDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SaleResponse>>(await unitOfWork.Sales.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(GetInvoiceFilter filter,
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
            cancellationToken)) ;
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