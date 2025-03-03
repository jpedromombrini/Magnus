using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;
[ApiController]
[Route("[controller]")]
[Authorize]
public class InvoicePaymentController(
    IInvoicePaymentAppService invoicePaymentAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<InvoicePaymentResponse>> GetAllInvoicePaymnetsAsync(
        CancellationToken cancellationToken)
    {
        return await invoicePaymentAppService.GetInvoicePaymentsAsync(cancellationToken);
    }

    [HttpGet("{invoiceId:guid}")]
    public async Task<InvoicePaymentResponse> GetInvoicesByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken)
    {
        return await invoicePaymentAppService.GetInvoicePaymentsByInvoiceIdAsync(invoiceId, cancellationToken);
    }
}