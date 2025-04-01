using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class SaleService(
    IProductStockService productStockService,
    IWarehouseService warehouseService,
    IAuditProductService auditProductService) : ISaleService
{
    public void CreateSale(Sale sale, Client client, User user)
    {
        sale.SetClientName(client.Name);
        sale.SetUserId(user.Id);
        sale.SetCreateAt(DateTime.Now);
        sale.SetStatus(SaleStatus.Open);
        if (sale is { Receipts.Count: > 0 })
        {
            ValidateReceipts(sale);
        }
    }

    public void UpdateSale(Sale sale, Client client, User user,  IEnumerable<SaleItem> items, IEnumerable<SaleReceipt> receipts, decimal value, decimal finantialDiscount)
    {
        UpdateItems(sale, items);
        UpdateReceipts(sale, receipts);
        sale.SetClientId(client.Id);
        sale.SetClientName(client.Name);
        sale.SetValue(value);
        sale.SetFinantialDiscount(finantialDiscount);
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
    private static void UpdateReceipts(Sale sale, IEnumerable<SaleReceipt> receipts)
    {
        sale.RemoveAllReceipts();
        foreach (var saleReceiptRequest in receipts)
        {
            var saleReceipt = new SaleReceipt(sale, saleReceiptRequest.Receipt);
            foreach (var installment in saleReceiptRequest.Installments)
                saleReceipt.AddInstallment(installment);
            sale.AddReceipt(saleReceipt);
        }
    }

    public async Task Invoice(Sale sale, CancellationToken cancellationToken)
    {
        ValidateReceipts(sale);
        if (sale.GetTotalItemValue() != sale.Value)
            throw new BusinessRuleException("Total do itens diferente do total do pedido");
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

    private async Task ValidateStockAsync(SaleItem item, int warehouseId, CancellationToken cancellationToken)
    {
        var stock = await productStockService.GetProductStockAsync(item.ProductId, warehouseId, cancellationToken);
        if(stock < item.Amount)
            throw new BusinessRuleException($"o Item {item.ProductName} sem estoque");
    }

    private static void ValidateReceipts(Sale sale)
    {
        var totalReceipts = sale.Receipts.Sum(r => r.Installments.Sum(i => i.GetRealValue()));
        if (totalReceipts < sale.GetRealValue())
        {
            throw new BusinessRuleException("Total do pedido maior que o valor dos recebimentos");
        }
    }
}