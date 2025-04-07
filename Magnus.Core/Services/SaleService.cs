using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class SaleService(
    IProductStockService productStockService,
    IWarehouseService warehouseService,
    IAuditProductService auditProductService,
    IClientService clientService,
    IUserService userService,
    ISaleReceiptService saleReceiptService,
    IUnitOfWork unitOfWork) : ISaleService
{
    public async Task CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var client = await clientService.GetByIdAsync(sale.ClientId, cancellationToken);
        if(client is null)
            throw new BusinessRuleException("Cliente não encontrado");
        var user = await userService.GetUserByIdAsync(sale.UserId, cancellationToken);
        if(user is null)
            throw new BusinessRuleException("Usuário não encontrado");
        
        sale.SetClientName(client.Name);
        sale.SetUserId(user.Id);
        sale.SetCreateAt(DateTime.Now);
        sale.SetStatus(SaleStatus.Open);
    }

    public async Task UpdateSale(
        Sale sale, 
        Client client,
        User user,  
        IEnumerable<SaleItem> items,
        IEnumerable<SaleReceipt> receipts, 
        decimal value, 
        decimal finantialDiscount, 
        CancellationToken cancellationToken)
    {
        sale.SetClientId(client.Id);
        sale.SetClientName(client.Name);
        sale.SetValue(value);
        sale.SetFinantialDiscount(finantialDiscount);
        ValidateFinantial(sale, receipts);
        UpdateItems(sale, items);
        await UpdateReceipts(sale, receipts, cancellationToken);
        
    }

    private static void UpdateItems(Sale sale, IEnumerable<SaleItem> items)
    {
        foreach (var item in items)
        {
            var existingItem = sale.Items.SingleOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.SetAmount(item.Amount);
                existingItem.SetDiscount(item.Discount);
                existingItem.SetValue(item.Value);
                existingItem.SetTotalPrice(item.TotalPrice);
            }
            else
            {
                sale.AddItem(item);
            }
        }
        var itemsToRemove = sale.Items
            .Where(existingItem => !items.Any(item => item.ProductId == existingItem.ProductId))
            .ToList();
        foreach (var itemToRemove in itemsToRemove)
        {
            sale.RemoveItem(itemToRemove);
        }
    }

    private async Task UpdateReceipts(Sale sale, IEnumerable<SaleReceipt> saleReceipts, CancellationToken cancellationToken)
    {
        await saleReceiptService.RevomeFromSaleAsync(sale.Id, cancellationToken);
        await saleReceiptService.AddRangeAsync(sale, saleReceipts, cancellationToken);
    }

    public async Task Invoice(Sale sale, CancellationToken cancellationToken)
    {
        if (sale.GetTotalItemValue() != sale.Value)
            throw new BusinessRuleException("Total do itens diferente do total do pedido");
        var receipts = await saleReceiptService.GetSaleReceiptsAsync(sale.Id, cancellationToken);
        if(!receipts.Any())
            throw new BusinessRuleException("Pedido sem financeiro");
        ValidateFinantial(sale, receipts);
        
        var warehouse = await warehouseService.GetByUserIdAsync(sale.UserId, cancellationToken);
        if (warehouse == null)
            throw new BusinessRuleException("Nenhum depósito encontrado");
        foreach (var item in sale.Items)
            await ValidateStockAsync(item, warehouse.Code, cancellationToken);
        foreach (var item in sale.Items)
            await productStockService.SubtractProductStockAsync(item.ProductId, warehouse.Code, item.Amount,
                cancellationToken);
        await auditProductService.SaleMovimentAsync(sale, warehouse.Code, cancellationToken);
        sale.SetStatus(SaleStatus.Invoiced);
    } 

    public async Task<Sale?> GetSaleByDocument(int documentId, CancellationToken cancellationToken)
    {
        return await unitOfWork.Sales.GetByExpressionAsync(x => x.Document == documentId, cancellationToken);
    }

    private async Task ValidateStockAsync(SaleItem item, int warehouseId, CancellationToken cancellationToken)
    {
        var stock = await productStockService.GetProductStockAsync(item.ProductId, warehouseId, cancellationToken);
        if(stock < item.Amount)
            throw new BusinessRuleException($"o Item {item.ProductName} sem estoque");
    }

    private static void ValidateFinantial(Sale sale, IEnumerable<SaleReceipt> receipts)
    {
        var totalInstallments = receipts
            .SelectMany(receipt => receipt.Installments) 
            .Sum(installment => installment.Value - installment.Discount + installment.Interest);
        if (sale.GetRealValue() - totalInstallments != 0)
            throw new BusinessRuleException("Total das parcelas diverge do total do pedido");
    }
    
}