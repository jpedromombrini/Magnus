using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class SellerController(
    ISellerAppService sellerAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<SellerResponse>> GetAllSellersAsync(CancellationToken cancellationToken)
    {
        return await sellerAppService.GetSellersAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<SellerResponse>> GetSellersByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await sellerAppService.GetSellersByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<SellerResponse> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await sellerAppService.GetSellerByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddSellerAsync([FromBody] CreateSellerRequest request, CancellationToken cancellationToken)
    {
        await sellerAppService.AddSellerAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateSellerAsync(Guid id, [FromBody] UpdateSellerRequest request,
        CancellationToken cancellationToken)
    {
        await sellerAppService.UpdateSellerAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteSellerAsync(Guid id, CancellationToken cancellationToken)
    {
        await sellerAppService.DeleteSellerAsync(id, cancellationToken);
    }
}