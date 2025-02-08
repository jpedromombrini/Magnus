using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WarehouseController(
    IWarehouseAppService warehouseAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<WarehouseResponse>> GetAllWarehousesAsync(CancellationToken cancellationToken)
    {
        return await warehouseAppService.GetWarehousesAsync(cancellationToken);
    }

    [HttpGet("getbyuserid/{userId:guid}")]
    public async Task<WarehouseResponse> GetWarehousesByFilterAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        return await warehouseAppService.GetWarehousesByFilterAsync(x => x.UserId == userId, cancellationToken);
    }
    
    [HttpGet("getbycode")]
    public async Task<WarehouseResponse> GetWarehousesByFilterAsync([FromQuery] int code,
        CancellationToken cancellationToken)
    {
        return await warehouseAppService.GetWarehousesByFilterAsync(x => x.Code == code, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<WarehouseResponse> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await warehouseAppService.GetWarehouseByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddWarehouseAsync([FromBody] CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        await warehouseAppService.AddWarehouseAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateWarehouseAsync(Guid id, [FromBody] UpdateWarehouseRequest request,
        CancellationToken cancellationToken)
    {
        await warehouseAppService.UpdateWarehouseAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken)
    {
        await warehouseAppService.DeleteWarehouseAsync(id, cancellationToken);
    }
}