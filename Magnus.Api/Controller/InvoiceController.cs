using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class InvoiceController(
    IInvoiceAppService invoiceAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<InvoiceResponse>> GetAllInvoicesAsync(CancellationToken cancellationToken)
    {
        return await invoiceAppService.GetInvoicesAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<InvoiceResponse>> GetInvoicesByFilterAsync(DateTime? initialDate,
        DateTime? finalDate,
        int number,
        int serie,
        string? key,
        Guid supplierId,
        CancellationToken cancellationToken)
    {
       
        return await invoiceAppService.GetInvoicesByFilterAsync(x =>
                (initialDate == null || x.DateEntry.Date >= initialDate)
                && (finalDate == null || x.DateEntry.Date <= finalDate)
                && (number == 0 || x.Number == number)
                && (serie == 0 || x.Serie == serie)
                && (string.IsNullOrEmpty(key) || x.Key == key)
                && (supplierId == Guid.Empty || x.SupplierId == supplierId),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<InvoiceResponse> GetInvoiceByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await invoiceAppService.GetInvoiceByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddInvoiceAsync([FromBody] CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        await invoiceAppService.AddInvoiceAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateInvoiceAsync(Guid id, [FromBody] UpdateInvoiceRequest request,
        CancellationToken cancellationToken)
    {
        await invoiceAppService.UpdateInvoiceAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteInvoiceAsync(Guid id, CancellationToken cancellationToken)
    {
        await invoiceAppService.DeleteInvoiceAsync(id, cancellationToken);
    }
}