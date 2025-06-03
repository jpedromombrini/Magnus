using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AuditProductController(IAuditProductAppService auditProductAppService) : ControllerBase
{
    [HttpGet("getbalance/{productId:guid}")]
    public async Task<int> GetBalanceAsync([FromRoute] Guid productId, [FromQuery] int warehouseId,
        CancellationToken cancellationToken)
    {
        return await auditProductAppService.GetBalanceAsync(productId, warehouseId, cancellationToken);
    }
}