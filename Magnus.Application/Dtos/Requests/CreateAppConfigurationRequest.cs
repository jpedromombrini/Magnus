namespace Magnus.Application.Dtos.Requests;

public record CreateAppConfigurationRequest(
    string CostCenterSale,
    int AmountToDiscount,
    int DaysValidityEstimate);