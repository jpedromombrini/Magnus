namespace Magnus.Application.Dtos.Requests;

public record UpdateAppConfigurationRequest(
    CostCenterRequest CostCenterSale,
    int AmountToDiscount,
    int DaysValidityEstimate);