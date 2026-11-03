using Association.Application.DTOs;
using Association.Application.Queries.GetAssociationDetailsById;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Associations;

[ApiController]
[Route("api/associations")]
[Authorize(Policy = "AssociationAdmin")]
public sealed class AssociationDetailsController(IQuerySender querySender) : ControllerBase
{
    [HttpGet("current")]
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
