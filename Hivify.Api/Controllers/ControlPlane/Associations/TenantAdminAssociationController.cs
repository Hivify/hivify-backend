using Association.Application.DTOs;
using Association.Application.Queries.GetAssociationDetailsById;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/tenant-admin/association")]
[Authorize(Policy = "TenantAdmin")]
public sealed class TenantAdminAssociationController(IQuerySender querySender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AssociationDto>> GetCurrentAssociation(
        CancellationToken ct)
    {
        var association = await querySender.Send(
            new GetCurrentAssociationDetailsQuery(),
            ct);

        if (association is null)
        {
            return NotFound();
        }

        return Ok(association);
    }
}