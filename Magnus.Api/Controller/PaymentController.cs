using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PaymentController(IPaymentAppService paymentAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<PaymentResponse>> GetAllPaymentsAsync(CancellationToken cancellationToken)
    {
        return await paymentAppService.GetPaymentsAsync(cancellationToken);
    }

    [HttpGet("getbyname")]
    public async Task<IEnumerable<PaymentResponse>> GetPaymentsByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return [];
        return await paymentAppService.GetPaymentsByFilterAsync(x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<PaymentResponse> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await paymentAppService.GetPaymentByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddPaymentAsync([FromBody] CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        await paymentAppService.AddPaymentAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdatePaymentAsync(Guid id, [FromBody] UpdatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        await paymentAppService.UpdatePaymentAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeletePaymentAsync(Guid id, CancellationToken cancellationToken)
    {
        await paymentAppService.DeletePaymentAsync(id, cancellationToken);
    }
}