using Association.Application.Commands.CreateAssociation;
using Association.Application.Queries.GetAllAssociationByUser;
using Association.Application.Queries.GetAllAssociations;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Platform;
using Hivify.Api.Controllers.ControlPlane.Associations.Mappers;
using Hivify.Api.Controllers.ControlPlane.Associations.Requests;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/platform/associations")]
[Authorize(Roles = "Admin")]
public sealed class PlatformAssociationsController(
    IQuerySender querySender,
    ICommandSender commandSender,
    IPlatformAuthorization authorization) : ControllerBase
{
    private readonly IPlatformAuthorization _authorization = authorization;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAssociation(
        [FromBody] CreateAssociationRequest request,
        CancellationToken ct)
    {
        await _authorization.EnsureApplicationAdminAsync(ct);

        var associationId = await commandSender.Send(
            new CreateAssociationCommand(
                request.Name,
                request.OwnerUserId,
                request.OwnerEmailAddress,
                request.TierId
                ),
            ct);

        return CreatedAtAction(
            nameof(GetAllAssociations),
            new { id = associationId },
            associationId);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AssociationResponse>>> GetAllAssociations(CancellationToken ct)
    {
        var associations = await querySender.Send(
            new GetAllAssociationsQuery(),
            ct);

        var response = associations
            .Select(AssociationResponseMapper.ToMyResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> GetAssociationsForUser(
        Guid userId,
        CancellationToken ct)
    {
        await _authorization.EnsureApplicationAdminAsync(ct);

        var associations = await querySender.Send(
            new GetAllAssociationsByUserQuery(userId),
            ct);

        return Ok(associations);
    }
}
