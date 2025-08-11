using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class CampaignService(
    IUnitOfWork unitOfWork) : ICampaignService
{
    public async Task CreateCampaign(Campaign campaign, CancellationToken cancellationToken)
    {
        await ValidateCampaignInterval(campaign, cancellationToken);
        await ValidateItems(campaign, cancellationToken);
    }

    public async Task UpdateCampaign(Guid id, Campaign campaign, CancellationToken cancellationToken)
    {
        var campaignDb = await unitOfWork.Campaigns.GetByIdAsync(id, cancellationToken);
        if (campaignDb == null)
            throw new EntityNotFoundException("Nenhuma campanha encontrada com esse Id");
        campaignDb.SetName(campaign.Name);
        campaignDb.SetDescription(campaign.Description);
        campaignDb.SetActive(campaign.Active);
        campaignDb.SetInitialDate(campaign.InitialDate);
        campaignDb.SetFinalDate(campaign.FinalDate);
        RemoveItems(campaignDb.Items);
        campaignDb.Items.Clear();
        foreach (var item in campaign.Items)
        {
            var newItem = new CampaignItem(item.ProductId, item.Price);
            campaignDb.AddItem(newItem);
        }

        await ValidateItems(campaignDb, cancellationToken);
        unitOfWork.Campaigns.Update(campaignDb);
    }

    private async Task ValidateCampaignInterval(Campaign campaign, CancellationToken cancellationToken)
    {
        foreach (var item in campaign.Items)
        {
            var existsConflict = await unitOfWork.Campaigns.ExistsCampaignWithProductInPeriodAsync(item.ProductId,
                campaign.InitialDate, campaign.FinalDate, cancellationToken);

            if (existsConflict)
                throw new InvalidOperationException(
                    $"Já existe uma campanha com o produto {item.ProductId} neste período.");
        }
    }

    private async Task ValidateItems(Campaign campaign, CancellationToken cancellationToken)
    {
        foreach (var item in campaign.Items)
        {
            var product = await unitOfWork.Products.GetByIdAsync(item.ProductId, cancellationToken);
            if (product == null)
                throw new EntityNotFoundException($"Nenhum produto encontrado com o Id {item.ProductId}");
            item.SetProduct(product);
        }
    }

    private void RemoveItems(IEnumerable<CampaignItem> items)
    {
        unitOfWork.Campaigns.RemoveItems(items);
    }
}