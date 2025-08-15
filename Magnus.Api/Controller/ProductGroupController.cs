using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductGroupController(
    IProductGroupAppService productGroupAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<ProductGroupResponse>> GetAllProductGroupsAsync(CancellationToken cancellationToken)
    {
        return await productGroupAppService.GetProductGroupsAsync(cancellationToken);
    }

    [HttpGet("getbyname")]
    public async Task<IEnumerable<ProductGroupResponse>> GetProductGroupsByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        return await productGroupAppService.GetProductGroupsByFilterAsync(
            x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<ProductGroupResponse> GetProductGroupByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await productGroupAppService.GetProductGroupByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddProductGroupAsync([FromBody] CreateProductGroupRequest request,
        CancellationToken cancellationToken)
    {
        await productGroupAppService.AddProductGroupAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateProductGroupAsync(Guid id, [FromBody] UpdateProductGroupRequest request,
        CancellationToken cancellationToken)
    {
        await productGroupAppService.UpdateProductGroupAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteProductGroupAsync(Guid id, CancellationToken cancellationToken)
    {
        await productGroupAppService.DeleteProductGroupAsync(id, cancellationToken);
    }
}