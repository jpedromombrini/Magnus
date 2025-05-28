namespace Magnus.Application.Dtos.Requests;

public record UpdateAppConfigurationRequest(
    string CostCenterSale,
    int AmountToDiscount,
    int DaysValidityEstimate);