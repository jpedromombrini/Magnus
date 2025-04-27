using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;
public record GetInvoiceFilter(
    DateTime InitialDate,
    DateTime FinalDate,
    Guid SupplierId,
    int Number,
    int Serie,
    string? Key);

    
