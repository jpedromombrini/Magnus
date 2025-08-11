using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class EstimateService(
    IUnitOfWork unitOfWork,
    IClientService clientService,
    IUserService userService,
    ISaleService saleService) : IEstimateService
{
    public async Task CreateEstimateAsync(Estimate estimate, CancellationToken cancellationToken)
    {
        await ValidateClient(estimate, cancellationToken);
        await ValidateUser(estimate, cancellationToken);
        estimate.ValidateTotals();
        estimate.SetEstimateStatus(EstimateStatus.Open);
        await unitOfWork.Estimates.AddAsync(estimate, cancellationToken);
    }

    public async Task UpdateEstimateAsync(Guid id, Estimate estimate, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken);
        if (estimateDb == null)
            throw new EntityNotFoundException("orçamento não encontrado com o Id informado");

        await ValidateClient(estimate, cancellationToken);
        await ValidateUser(estimate, cancellationToken);
        estimate.ValidateTotals();
        estimateDb.SetDescription(estimate.Description);
        if (estimate.FreightId is not null)
            estimateDb.SetFreightId((Guid)estimate.FreightId);
        estimateDb.SetFreight(estimate.Freight);
        estimateDb.SetObservation(estimate.Observation);
        estimateDb.SetCreatedAt(estimate.CreatedAt);
        estimateDb.SetValue(estimate.Value);
        estimateDb.SetValidity(estimate.ValiditAt);
        estimateDb.SetFinantialDiscount(estimate.FinantialDiscount);
        estimateDb.SetClientId(estimate.ClientId);
        estimateDb.SetClientName(estimate.ClientName);
        UpdateItems(estimate, estimateDb);
        if (estimate.Receipts is not null)
            UpdateReceipts(estimateDb, estimate.Receipts);
        unitOfWork.Estimates.Update(estimateDb);
    }

    public async Task CreateSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken);
        if (estimateDb == null)
            throw new EntityNotFoundException("orçamento não encontrado com o Id informado");
        var saleExists =
            await unitOfWork.Sales.GetByExpressionAsync(x => x.EstimateId == estimateDb.Id, cancellationToken);
        if (saleExists is not null)
            throw new BusinessRuleException(
                $"Já existe um pedido criado com esse orçamento, pedido: {saleExists.Document}");
        if (estimateDb.ClientId is null || estimateDb.ClientId == Guid.Empty)
            throw new BusinessRuleException("Informe um cliente para o orçamento");
        var client = await clientService.GetByIdAsync((Guid)estimateDb.ClientId, cancellationToken);
        if (client is null)
            throw new EntityNotFoundException("cliente não encontrado com esse Id");
        var saleItems = estimateDb.Items.Select(item =>
                new SaleItem(item.ProductId, item.ProductName, item.Amount, item.Value, item.TotalValue, item.Discount))
            .ToList();
        var saleReceipts = new List<SaleReceipt>();
        if (estimateDb.Receipts is not null)
            foreach (var saleReceipt in estimateDb.Receipts)
            {
                var receipt = new SaleReceipt(client.Id, saleReceipt.UserId, saleReceipt.ReceiptId);
                foreach (var installment in saleReceipt.Installments)
                    receipt.AddInstallment(new SaleReceiptInstallment(installment.DueDate,
                        installment.PaymentDate, installment.PaymentValue, installment.Value, installment.Discount,
                        installment.Interest, installment.Installment, null));

                saleReceipts.Add(receipt);
            }

        var sale = new Sale(client.Id, estimateDb.UserId, estimateDb.Value, estimateDb.Freight,
            estimateDb.FinantialDiscount);
        if (estimateDb.FreightId is not null)
        {
            sale.SetFreight(estimateDb.Freight);
            sale.SetFreightId((Guid)estimateDb.FreightId);
        }

        sale.AddRangeReceipts(saleReceipts);
        sale.AddItems(saleItems);
        sale.SetClientName(client.Name);
        sale.SetStatus(SaleStatus.Invoiced);
        sale.SetEstimateId(estimateDb.Id);
        await unitOfWork.Sales.AddAsync(sale, cancellationToken);
        await saleService.Invoice(sale, cancellationToken);
        estimateDb.SetEstimateStatus(EstimateStatus.Invoiced);
    }

    private void UpdateReceipts(Estimate estimate, IEnumerable<EstimateReceipt> receipts)
    {
        if (estimate.Receipts is not null)
            unitOfWork.Estimates.DeleteReceiptRange(estimate.Receipts);
        estimate.AddRangeReceipts(receipts);
    }

    private static void UpdateItems(Estimate estimate, Estimate estimateDb)
    {
        foreach (var itemRequest in estimate.Items)
        {
            var existingItem = estimateDb.Items.FirstOrDefault(item => item.ProductId == itemRequest.ProductId);
            if (existingItem is not null)
            {
                existingItem.SetProductName(itemRequest.ProductName);
                existingItem.SetAmount(itemRequest.Amount);
                existingItem.SetDiscount(itemRequest.Discount);
                existingItem.setValue(itemRequest.Value);
                existingItem.SetTotalValue(itemRequest.TotalValue);
            }
            else
            {
                var item = new EstimateItem(
                    itemRequest.ProductId,
                    itemRequest.ProductName,
                    itemRequest.Amount,
                    itemRequest.Value,
                    itemRequest.TotalValue,
                    itemRequest.Discount);
                item.SetEstimateId(estimateDb.Id);
                estimateDb.AddItem(item);
            }
        }

        var itemsToRemove = estimateDb.Items
            .Where(item => estimate.Items.All(requestItem => requestItem.ProductId != item.ProductId))
            .ToList();
        foreach (var itemToRemove in itemsToRemove) estimateDb.Items.Remove(itemToRemove);
    }

    private async Task ValidateClient(Estimate estimate, CancellationToken cancellationToken)
    {
        if (estimate.ClientId != null)
        {
            var client = await clientService.GetByIdAsync((Guid)estimate.ClientId, cancellationToken);
            if (client == null)
                throw new BusinessRuleException("Cliente informado não encontrado");
        }
    }

    private async Task ValidateUser(Estimate estimate, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserByIdAsync(estimate.UserId, cancellationToken);
        if (user == null)
            throw new BusinessRuleException("Usuário informado não encontrado");
    }

    private void DeleteItems(IEnumerable<EstimateItem> estimateItems)
    {
        unitOfWork.Estimates.DeleteItensRange(estimateItems);
    }

    private async Task AddItemsAsync(IEnumerable<EstimateItem> estimateItems, CancellationToken cancellationToken)
    {
        await unitOfWork.Estimates.AddItensRangeAsync(estimateItems, cancellationToken);
    }
}