using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class EstimateService(
    IUnitOfWork unitOfWork,
    IClientService clientService,
    IUserService userService) : IEstimateService
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
        var estimateDb = await ValidateEstimate(id, cancellationToken);
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

    public async Task<Estimate> ValidateEstimate(Guid estimateId, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(estimateId, cancellationToken);
        if (estimateDb == null)
            throw new EntityNotFoundException("orçamento não encontrado com o Id informado");
        return estimateDb;
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
            await clientService.ValidateClientAsync((Guid)estimate.ClientId, cancellationToken);
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