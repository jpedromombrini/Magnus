using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LaboratoryController(
    ILaboratoryAppService laboratoryAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<LaboratoryResponse>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await laboratoryAppService.GetLaboratoriesAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<LaboratoryResponse>> GetProductsByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await laboratoryAppService.GetLaboratoriesByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<LaboratoryResponse> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await laboratoryAppService.GetLaboratoryByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddProductAsync([FromBody] CreateLaboratoryRequest request, CancellationToken cancellationToken)
    {
        await laboratoryAppService.AddLaboratoryAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateProductAsync(Guid id, [FromBody] UpdateLaboratoryRequest request,
        CancellationToken cancellationToken)
    {
        await laboratoryAppService.UpdateLaboratoryAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        await laboratoryAppService.DeleteLaboratoryAsync(id, cancellationToken);
    }
}