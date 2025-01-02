using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController(
    IProductAppService productAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await productAppService.GetProductsAsync(cancellationToken);
    }

    [HttpGet("GetByName")]
    public async Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return [];
        return await productAppService.GetProductsByFilterAsync(x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<ProductResponse> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await productAppService.GetProductByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddProductAsync([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        await productAppService.AddProductAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateProductAsync(Guid id, [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        await productAppService.UpdateProductAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        await productAppService.DeleteProductAsync(id, cancellationToken);
    }
}