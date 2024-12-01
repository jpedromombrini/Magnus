using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReceiptController(
    IReceiptAppService receiptAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<ReceiptResponse>> GetAllReceiptsAsync(CancellationToken cancellationToken)
    {
        return await receiptAppService.GetReceiptsAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<ReceiptResponse>> GetReceiptsByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await receiptAppService.GetReceiptsByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<ReceiptResponse> GetReceiptByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await receiptAppService.GetReceiptByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddReceiptAsync([FromBody] CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        await receiptAppService.AddReceiptAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateReceiptAsync(Guid id, [FromBody] UpdateReceiptRequest request,
        CancellationToken cancellationToken)
    {
        await receiptAppService.UpdateReceiptAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        await receiptAppService.DeleteReceiptAsync(id, cancellationToken);
    }
}