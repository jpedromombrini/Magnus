using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Magnus.Core.Enumerators;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class TransferWarehouseController(
    ITransferWarehouseAppService transferWarehouseAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<TransferWarehouseResponse>> GetTransferWarehousesByFilterAsync(
        [FromQuery] DateTime initialDate,
        [FromQuery] DateTime finalDate,
        [FromQuery] int warehouseId,
        CancellationToken cancellationToken)
    {
        return await transferWarehouseAppService.GetTransferWarehouseByFilterAsync(x =>
                x.CreatedAt >= initialDate
                && x.CreatedAt <= finalDate
                && (warehouseId == 0 || (x.WarehouseOriginId == warehouseId || x.WarehouseDestinyId == warehouseId)),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<TransferWarehouseResponse> GetTransferWarehouseByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await transferWarehouseAppService.GetTransferWarehouseByIdAsync(id, cancellationToken);
    }

    [HttpGet]
    [Route("getitembystatus")]
    public async Task<IEnumerable<TransferWarehouseItemResponse>> GetTransferWarehouseItemByFilterAsync(
        DateTime initialDate,
        DateTime finalDate,
        TransferWarehouseItemStatus status,
        int warehouseOrigin,
        int warehouseDestiny, CancellationToken cancellationToken)
    {
        return await transferWarehouseAppService.GetTransferWarehouseItemsByFilterAsync(x => x.Status == status
                && (warehouseOrigin == 0 || x.TransferWarehouse.WarehouseOriginId == warehouseOrigin)
                && (warehouseDestiny == 0 || x.TransferWarehouse.WarehouseDestinyId == warehouseDestiny), cancellationToken);
    }

    [HttpPost]
    public async Task AddTransferWarehouseAsync([FromBody] CreateTransferWarehouseRequest request,
        CancellationToken cancellationToken)
    {
        await transferWarehouseAppService.AddTransferWarehouseAsync(request, cancellationToken);
    }
    
    [HttpPatch]
    [Route("updatestatusitem/{id:guid}")]
    public async Task UpdateStatusTransferWarehouseItemAsync(Guid id, [FromBody] UpdateStatusTransferWarehouseItemRequest request,
        CancellationToken cancellationToken)
    {
        await transferWarehouseAppService.UpdateTransferWarehouseItemStatusAsync(id, request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateTransferWarehouseAsync(Guid id, [FromBody] UpdateTransferWarehouseRequest request,
        CancellationToken cancellationToken)
    {
        await transferWarehouseAppService.UpdateTransferWarehouseAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteTransferWarehouseAsync(Guid id, CancellationToken cancellationToken)
    {
        await transferWarehouseAppService.DeleteTransferWarehouseAsync(id, cancellationToken);
    }
}