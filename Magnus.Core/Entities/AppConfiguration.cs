namespace Magnus.Core.Entities;

public class AppConfiguration : EntityBase
{
    public string CostCenterSale { get; private set; }

    private AppConfiguration(){}

    public AppConfiguration(string costCenterSale)
    {
        SetCostCenterSale(costCenterSale);
    }

    public void SetCostCenterSale(string costCenterSale)
    {
        if(string.IsNullOrEmpty(costCenterSale))
            throw new ArgumentNullException(nameof(costCenterSale));
        CostCenterSale = costCenterSale;
    }
}