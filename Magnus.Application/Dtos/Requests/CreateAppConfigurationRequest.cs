namespace Magnus.Application.Dtos.Requests;

public record CreateAppConfigurationRequest(
    CostCenterRequest CostCenterSale,
    int AmountToDiscount,
    int DaysValidityEstimate);