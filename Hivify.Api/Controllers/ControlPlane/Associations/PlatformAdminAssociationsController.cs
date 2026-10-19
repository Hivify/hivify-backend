
using Association.Application.Queries.GetAllAssociations;
using BuildingBlocks.ApplicationPorts.Messaging;
using Hivify.Api.Controllers.ControlPlane.Associations.Mappers;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/platform-admin/associations")]
[Authorize(Roles = "Admin")]
public sealed class PlatformAdminAssociationsController(IQuerySender querySender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AssociationResponse>>>
        GetAllAssociations(CancellationToken ct)
    {
        var associations = await querySender.Send(
            new GetAllAssociationsQuery(),
            ct);

        var response = associations
            .Select(AssociationResponseMapper.ToMyResponse)
            .ToList();

        return Ok(response);
    }
}
