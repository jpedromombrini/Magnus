using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Helpers;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class SaleService(
    IProductStockService productStockService,
    IWarehouseService warehouseService,
    IAuditProductService auditProductService,
    IClientService clientService,
    IUserService userService,
    IAccountsReceivableService accountsReceivableService,
    IAppConfigurationService configurationService,
    IUnitOfWork unitOfWork) : ISaleService
{
    public async Task<Sale> CreateSaleByEstimateAsync(Estimate estimate, CancellationToken cancellationToken)
    {
        await ValidateSaleExistWithEstimateIdAsync(estimate.Id, cancellationToken);
        var client = await ValidateClientEstimate(estimate, cancellationToken);
        var saleItems = estimate.Items.Select(item =>
                new SaleItem(item.ProductId, item.ProductName, item.Amount, item.Value, item.TotalValue, item.Discount))
            .ToList();
        var saleReceipts = CreateSaleReceipts(client.Id, estimate.Receipts);
        var sale = new Sale(client.Id, estimate.UserId, estimate.Value, estimate.Freight,
            estimate.FinantialDiscount);
        if (estimate.FreightId is not null)
        {
            sale.SetFreight(estimate.Freight);
            sale.SetFreightId((Guid)estimate.FreightId);
        }

        sale.AddRangeReceipts(saleReceipts);
        sale.AddItems(saleItems);
        sale.SetClientName(client.Name);
        sale.SetStatus(SaleStatus.Invoiced);
        sale.SetEstimateId(estimate.Id);
        return sale;
    }

    public async Task CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var client = await clientService.ValidateClientAsync(sale.ClientId, cancellationToken);
        var user = await userService.GetUserByIdAsync(sale.UserId, cancellationToken);
        if (user is null)
            throw new BusinessRuleException("Usuário não encontrado");

        sale.SetClientName(client.Name);
        sale.SetUserId(user.Id);
        sale.SetCreateAt(DateTimeHelper.NowInBrasilia());
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

        var estimateDb = await unitOfWork.Estimates.GetByIdAsync((Guid)sale.EstimateId, cancellationToken);
        if (estimateDb is null)
            throw new BusinessRuleException("orçamento não encontrado");
        estimateDb.SetEstimateStatus(EstimateStatus.Open);
        sale.SetEstimateId(Guid.Empty);
    }

    public async Task Invoice(Sale sale, Client client, CancellationToken cancellationToken)
    {
        if (sale.GetTotalItemValue() != sale.Value)
            throw new BusinessRuleException("Total do itens diferente do total do pedido");

        ValidateFinantial(sale);
        var warehouse = await ValidateWarehouseAsync(sale.UserId, cancellationToken);
        await MovimentStockAsync(sale, warehouse, cancellationToken);

        await auditProductService.SaleMovimentAsync(sale, warehouse.Code, cancellationToken);

        await GenerateAccountsReceivable(client, sale.Document, sale.Receipts, cancellationToken);
        sale.SetStatus(SaleStatus.Invoiced);
    }

    private async Task ValidateSaleExistWithEstimateIdAsync(Guid estimateId, CancellationToken cancellationToken)
    {
        var sale = await unitOfWork.Sales.GetByExpressionAsync(x => x.EstimateId == estimateId, cancellationToken);
        if (sale is not null)
            throw new BusinessRuleException($"Já existe um pedido criado com esse orçamento, pedido: {sale.Document}");
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
        if (sale.Receipts is null || sale.Receipts.Count == 0)
            throw new BusinessRuleException("Pedido sem financeiro");

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
        if (configuration.CostCenterSale is null)
            throw new BusinessRuleException("Informe o centro de custo");
        List<AccountsReceivable> accountsReceivables = [];
        foreach (var saleReceipt in saleReceipts)
        {
            var receipt = await unitOfWork.Receipts.GetByIdAsync(saleReceipt.ReceiptId, cancellationToken);
            foreach (var installment in saleReceipt.Installments)
            {
                var account = new AccountsReceivable(saleReceipt.ClienteId, installment.Id, document,
                    installment.DueDate,
                    installment.Value, installment.Interest, installment.Discount, installment.Installment,
                    saleReceipt.Installments.Count(), (Guid)configuration.CostCenterSaleId);
                account.SetClient(client);
                account.SetCostCenter(configuration.CostCenterSale);
                account.SetCostCenterId((Guid)configuration.CostCenterSaleId);
                account.SetReceipt(receipt);
                account.SetReceiptId(receipt.Id);
                accountsReceivables.Add(account);
            }
        }


        await accountsReceivableService.CreateAsync(accountsReceivables, client, cancellationToken);
    }

    private async Task<Client> ValidateClientEstimate(Estimate estimate, CancellationToken cancellationToken)
    {
        if (estimate.ClientId is null || estimate.ClientId == Guid.Empty)
            throw new BusinessRuleException("Informe um cliente para o orçamento");
        var client = await clientService.ValidateClientAsync((Guid)estimate.ClientId, cancellationToken);
        if (client is null)
            throw new EntityNotFoundException("cliente não encontrado com esse Id");
        return client;
    }

    private List<SaleReceipt> CreateSaleReceipts(Guid clientId, ICollection<EstimateReceipt>? receipts)
    {
        var saleReceipts = new List<SaleReceipt>();
        if (receipts is null)
            throw new BusinessRuleException("O orçamento não tem financeiro");
        foreach (var saleReceipt in receipts)
        {
            var receipt = new SaleReceipt(clientId, saleReceipt.UserId, saleReceipt.ReceiptId);
            foreach (var installment in saleReceipt.Installments)
                receipt.AddInstallment(new SaleReceiptInstallment(installment.DueDate,
                    installment.PaymentDate, installment.PaymentValue, installment.Value, installment.Discount,
                    installment.Interest, installment.Installment, null));

            saleReceipts.Add(receipt);
        }

        return saleReceipts;
    }

    private async Task<Warehouse> ValidateWarehouseAsync(Guid userId, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseService.GetByUserIdAsync(userId, cancellationToken);
        if (warehouse == null)
            throw new BusinessRuleException("Nenhum depósito encontrado");
        return warehouse;
    }

    private async Task MovimentStockAsync(Sale sale, Warehouse warehouse, CancellationToken cancellationToken)
    {
        foreach (var item in sale.Items)
            await ValidateStockAsync(item, warehouse.Code, cancellationToken);
        foreach (var item in sale.Items)
            await productStockService.SubtractProductStockAsync(item.ProductId, warehouse, item.Amount,
                cancellationToken);
    }
}