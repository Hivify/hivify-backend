using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiering.Application.Contracts;

namespace Hivify.Api.Controllers.ControlPlane.Tiering;

[Route("api/platform-admin/tiers")]
[ApiController]
[Authorize(Roles = "Admin")]
public sealed class TierController(ITierService tierService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTiers(
        CancellationToken ct)
    {
        var tiers = await tierService.GetAllTiersAsync(ct);

        return Ok(tiers);
    }
}