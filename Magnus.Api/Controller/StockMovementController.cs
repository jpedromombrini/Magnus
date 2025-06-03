using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class StockMovementController(IStockMovementAppService stockMovementAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<StockMovementResponse>> GetAllStockMovementsAsync(CancellationToken cancellationToken)
    {
        return await stockMovementAppService.GetStockMovementsAsync(cancellationToken);
    }

    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<StockMovementResponse>> GetStockMovementsByFilterAsync(
        [FromQuery] GetStockMovementFilter filter,
        CancellationToken cancellationToken)
    {
        return await stockMovementAppService.GetStockMovementsByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<StockMovementResponse> GetStockMovementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await stockMovementAppService.GetStockMovementByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddStockMovementAsync([FromBody] CreateStockMovementRequest request,
        CancellationToken cancellationToken)
    {
        await stockMovementAppService.AddStockMovementAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateStockMovementAsync(Guid id, [FromBody] UpdateStockMovementRequest request,
        CancellationToken cancellationToken)
    {
        await stockMovementAppService.UpdateStockMovementAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteStockMovementAsync(Guid id, CancellationToken cancellationToken)
    {
        await stockMovementAppService.DeleteStockMovementAsync(id, cancellationToken);
    }
}