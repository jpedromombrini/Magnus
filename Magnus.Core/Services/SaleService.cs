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
    IAccountsReceivableService accountsReceivableService,
    IAppConfigurationService configurationService,
    IUnitOfWork unitOfWork) : ISaleService
{
    public async Task CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var client = await clientService.GetByIdAsync(sale.ClientId, cancellationToken);
        if (client is null)
            throw new BusinessRuleException("Cliente não encontrado");
        var user = await userService.GetUserByIdAsync(sale.UserId, cancellationToken);
        if (user is null)
            throw new BusinessRuleException("Usuário não encontrado");

        sale.SetClientName(client.Name);
        sale.SetUserId(user.Id);
        sale.SetCreateAt(DateTime.Now);
        sale.SetStatus(SaleStatus.Open);
    }

    public async Task UpdateSale(Guid id, Sale sale, CancellationToken cancellationToken)
    {
        var saleDb = await unitOfWork.Sales.GetByIdAsync(id, cancellationToken);
        if (saleDb == null)
            throw new EntityNotFoundException(id.ToString());
        var clientDb = await unitOfWork.Clients.GetByIdAsync(sale.ClientId, cancellationToken);
        if (clientDb == null)
            throw new EntityNotFoundException("Cliente não encontrado");
        var userDb = await unitOfWork.Users.GetByIdAsync(sale.UserId, cancellationToken);
        if (userDb == null)
            throw new EntityNotFoundException("usuário não encontrado");
        sale.SetClientId(clientDb.Id);
        sale.SetClientName(clientDb.Name);
        sale.SetValue(sale.Value);
        sale.SetFinantialDiscount(sale.FinantialDiscount);
        ValidateFinantial(sale);
        await UpdateItems(saleDb, sale.Items, cancellationToken);
        await UpdateReceipts(saleDb, sale.Receipts, cancellationToken);
    }

    public async Task Invoice(Sale sale, CancellationToken cancellationToken)
    {
        var client = await clientService.GetByIdAsync(sale.ClientId, cancellationToken);
        if (client is null)
            throw new BusinessRuleException("Cliente não encontrado");
        if (sale.GetTotalItemValue() != sale.Value)
            throw new BusinessRuleException("Total do itens diferente do total do pedido");
        var receipts = await saleReceiptService.GetSaleReceiptsAsync(sale.Id, cancellationToken);
        if (receipts is null)
            throw new BusinessRuleException("Pedido sem financeiro");
        ValidateFinantial(sale);

        var warehouse = await warehouseService.GetByUserIdAsync(sale.UserId, cancellationToken);
        if (warehouse == null)
            throw new BusinessRuleException("Nenhum depósito encontrado");
        foreach (var item in sale.Items)
            await ValidateStockAsync(item, warehouse.Code, cancellationToken);
        foreach (var item in sale.Items)
            await productStockService.SubtractProductStockAsync(item.ProductId, warehouse, item.Amount,
                cancellationToken);
        await auditProductService.SaleMovimentAsync(sale, warehouse.Code, cancellationToken);
        await GenerateAccountsReceivable(client, sale.Document, receipts, cancellationToken);
        sale.SetStatus(SaleStatus.Invoiced);
    }

    public async Task<Sale?> GetSaleByDocument(int documentId, CancellationToken cancellationToken)
    {
        return await unitOfWork.Sales.GetByExpressionAsync(x => x.Document == documentId, cancellationToken);
    }

    public async Task CancelSale(Sale sale, string reason, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseService.GetByUserIdAsync(sale.UserId, cancellationToken);
        if (warehouse is null)
            throw new BusinessRuleException("Depósito não encontrado para o usuário");
        await RemoveInstallments(sale, cancellationToken);
        await auditProductService.CancelSaleMovimentAsync(sale, cancellationToken);
        foreach (var item in sale.Items)
            await productStockService.IncrementProductStockAsync(item.ProductId, warehouse, item.Amount,
                cancellationToken);
        sale.SetReasonCancel(reason);
        sale.SetStatus(SaleStatus.Canceled);
    }

    private async Task UpdateItems(Sale sale, IEnumerable<SaleItem> items, CancellationToken cancellationToken)
    {
        unitOfWork.Sales.DeleteItensRange(sale.Items);
        await unitOfWork.Sales.AddItemsRangeAsync(items, cancellationToken);
    }

    private async Task UpdateReceipts(Sale sale, IEnumerable<SaleReceipt>? saleReceipts,
        CancellationToken cancellationToken)
    {
        if (sale.Receipts is not null)
            unitOfWork.Sales.DeleteReceiptsRange(sale.Receipts);
        if (saleReceipts is not null)
            await unitOfWork.Sales.AddReceiptsRangeAsync(saleReceipts, cancellationToken);
    }

    private async Task RemoveInstallments(Sale sale, CancellationToken cancellationToken)
    {
        var accountsToRemove = new List<AccountsReceivable>();
        if (sale.Receipts is not null)
        {
            foreach (var installment in sale.Receipts.SelectMany(x => x.Installments))
            {
                var acconutsReceivable =
                    await accountsReceivableService.GetBySaleReceiptInstallmentIdAsync(installment.Id,
                        cancellationToken);
                if (acconutsReceivable is not null)
                {
                    accountsToRemove.Add(acconutsReceivable);
                    if (acconutsReceivable.Status == AccountsReceivableStatus.Paid)
                        throw new BusinessRuleException("Existe um título baixado referente a essa venda");
                }
            }

            accountsReceivableService.RemoveRangeAsync(accountsToRemove);
        }
    }

    private async Task ValidateStockAsync(SaleItem item, int warehouseId, CancellationToken cancellationToken)
    {
        var stock = await productStockService.GetProductStockAsync(item.ProductId, warehouseId, cancellationToken);
        if (stock < item.Amount)
            throw new BusinessRuleException($"o Item {item.ProductName} sem estoque");
    }

    private static void ValidateFinantial(Sale sale)
    {
        var totalInstallments = sale.Receipts
            .SelectMany(receipt => receipt.Installments)
            .Sum(installment => installment.Value);
        if (sale.GetRealValue() - totalInstallments != 0)
            throw new BusinessRuleException("Total das parcelas diverge do total do pedido");
    }

    private async Task GenerateAccountsReceivable(Client client, int document, IEnumerable<SaleReceipt> saleReceipts,
        CancellationToken cancellationToken)
    {
        var configuration = await configurationService.GetAppConfigurationAsync(cancellationToken);
        foreach (var saleReceipt in saleReceipts)
        foreach (var installment in saleReceipt.Installments)
        {
            DateOnly? paymentDate = installment.PaymentDate is DateTime dt ? DateOnly.FromDateTime(dt) : null;
            var account = new AccountsReceivable(installment.Id, client.Id, client.Name, document,
                installment.DueDate, paymentDate, installment.PaymentValue, installment.Value, installment.Interest,
                installment.Discount, installment.Installment, configuration.CostCenterSale);
            await accountsReceivableService.CreateAsync(account, cancellationToken);
        }
    }
}