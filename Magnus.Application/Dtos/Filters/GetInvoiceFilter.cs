namespace Magnus.Application.Dtos.Filters;

public record GetInvoiceFilter(
    DateTime? InitialDate,
    DateTime? FinalDate,
    int Number,
    int Serie,
    string? Key,
    Guid SupplierId);