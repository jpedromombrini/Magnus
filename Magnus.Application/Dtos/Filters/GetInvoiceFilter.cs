using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;
public record GetInvoiceFilter(
    DateTime InitialDate,
    DateTime FinalDate,
    int Document,
    Guid ClientId,
    Guid UserId,
    SaleStatus Status);

    
