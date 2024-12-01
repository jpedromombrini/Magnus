using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SupplierController(
    ISupplierAppService supplierAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<SupplierResponse>> GetAllSuppliersAsync(CancellationToken cancellationToken)
    {
        return await supplierAppService.GetSuppliersAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<SupplierResponse>> GetSuppliersByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await supplierAppService.GetSuppliersByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<SupplierResponse> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await supplierAppService.GetSupplierByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddSupplierAsync([FromBody] CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        await supplierAppService.AddSupplierAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateSupplierAsync(Guid id, [FromBody] UpdateSupplierRequest request,
        CancellationToken cancellationToken)
    {
        await supplierAppService.UpdateSupplierAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteSupplierAsync(Guid id, CancellationToken cancellationToken)
    {
        await supplierAppService.DeleteSupplierAsync(id, cancellationToken);
    }
}