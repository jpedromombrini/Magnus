namespace Magnus.Core.Entities;

public class AppConfiguration : EntityBase
{
    private AppConfiguration()
    {
    }

    public AppConfiguration(string costCenterSale, int amountToDiscount, int daysValidityEstimate)
    {
        SetCostCenterSale(costCenterSale);
        SetAmountToDiscount(amountToDiscount);
        SetDaysValidityEstimate(daysValidityEstimate);
    }

    public string CostCenterSale { get; private set; }
    public int AmountToDiscount { get; private set; }
    public int DaysValidityEstimate { get; private set; }

    public void SetCostCenterSale(string costCenterSale)
    {
        if (string.IsNullOrEmpty(costCenterSale))
            throw new ArgumentNullException(nameof(costCenterSale));
        CostCenterSale = costCenterSale;
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