using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Identity
{
    [ApiController]
    [Route("api/debug")]
    [Authorize]
    public sealed class ClaimsDebugController : ControllerBase
    {
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }));
        }
    }
}

