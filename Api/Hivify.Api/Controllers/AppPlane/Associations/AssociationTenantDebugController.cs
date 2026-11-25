using Finbuckle.MultiTenant.AspNetCore.Extensions;
using Hivify.Api.MultiTenancy;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.AppPlane.Associations;

[ApiController]
[Route("api/associations/debug")]
public sealed class AssociationTenantDebugController : ControllerBase
{
    [HttpGet("tenant")]
    public IActionResult GetTenant()
    {
        var tenantInfo = HttpContext
            .GetMultiTenantContext<ExtendedTenantInfo>()
            .TenantInfo;

        if (tenantInfo is null)
        {
            return NotFound(new
            {
                Message = "No tenant was resolved for this request."
            });
        }

        return Ok(new
        {
            tenantInfo.Id,
            tenantInfo.Identifier,
            tenantInfo.Name
        });
    }
}
