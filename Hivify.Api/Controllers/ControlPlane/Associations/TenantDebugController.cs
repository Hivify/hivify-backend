using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations
{
    [ApiController]
    [Route("api/debug")]
    public sealed class TenantDebugController : ControllerBase
    {
        [HttpGet("tenant")]
        public IActionResult GetTenant()
        {
            var tenantInfo = HttpContext
                .GetMultiTenantContext<TenantInfo>()
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
}
