namespace Magnus.Core.Entities;

public class AppConfiguration : EntityBase
{
    private AppConfiguration()
    {
    }

    public AppConfiguration(Guid costCenterSaleId, int amountToDiscount, int daysValidityEstimate)
    {
        SetCostCenterSaleId(costCenterSaleId);
        SetAmountToDiscount(amountToDiscount);
        SetDaysValidityEstimate(daysValidityEstimate);
    }

    public Guid? CostCenterSaleId { get; private set; }
    public CostCenter? CostCenterSale { get; private set; }
    public int AmountToDiscount { get; private set; }
    public int DaysValidityEstimate { get; private set; }

    public void SetCostCenterSale(CostCenter costCenterSale)
    {
        ArgumentNullException.ThrowIfNull(costCenterSale);
        CostCenterSale = costCenterSale;
        CostCenterSaleId = costCenterSale.Id;
    }

    public void SetCostCenterSaleId(Guid costCenterSaleId)
    {
        if (costCenterSaleId == Guid.Empty)
            throw new ArgumentNullException(nameof(costCenterSaleId));
        CostCenterSaleId = costCenterSaleId;
    }

    public void SetAmountToDiscount(int amountToDiscount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amountToDiscount);
        AmountToDiscount = amountToDiscount;
    }

    public void SetDaysValidityEstimate(int daysValidityEstimate)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(daysValidityEstimate);
        DaysValidityEstimate = daysValidityEstimate;
    }
}