namespace Magnus.Application.Dtos.Filters;

public record SalesBySellerFilter(DateOnly InitialDate, DateOnly FinalDate);