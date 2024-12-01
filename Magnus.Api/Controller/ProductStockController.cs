using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductStockController(
    IProductStockAppService productStockAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ProductStockResponse>> GetProductStocksByFilterAsync(
        [FromQuery] Guid productId,
        [FromQuery] int warehouseId,
        [FromQuery] bool all,
        CancellationToken cancellationToken)
    {
        return await productStockAppService.GetProductStocksByFilterAsync(productId, warehouseId, all,
            cancellationToken);
    }

    [HttpGet]
    [Route("getbalance/{productId}")]
    public async Task<decimal> GetBalanceProductStocksByFilterAsync([FromQuery] Guid productId,
        [FromQuery] int warehouseId,
        CancellationToken cancellationToken)
    {
        return await productStockAppService.GetBalanceProductStocksAsync(productId, warehouseId, cancellationToken);
    }
}