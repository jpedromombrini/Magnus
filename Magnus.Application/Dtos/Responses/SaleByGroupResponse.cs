namespace Magnus.Application.Dtos.Responses;

public record SaleByGroupResponse(
    string GroupName,
    int Amount,
    decimal TotalValue,
    IEnumerable<SaleProductByGroup> Products);

public record SaleProductByGroup(string ProductName, int Amount, decimal TotalValue);