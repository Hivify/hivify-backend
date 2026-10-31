using Association.Application.Commands.CreateAssociation;
using Association.Application.Queries.GetAllAssociations;
using Association.Application.Queries.GetUserAssociations;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.HivifyUser;
using Hivify.Api.Controllers.ControlPlane.Associations.Mappers;
using Hivify.Api.Controllers.ControlPlane.Associations.Requests;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/platform-admin/associations")]
[Authorize(Roles = "Admin")]
public sealed class PlatformAdminAssociationsController(
    IQuerySender querySender,
    ISender commandSender,
    IHivifyUserAuthorization authorization
    ) : ControllerBase
{

    private readonly IHivifyUserAuthorization _authorization = authorization;


    /// <summary>
    ///     create an association 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
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
                request.OwnerEmailAddress),
            ct);

        return CreatedAtAction(
            nameof(GetAllAssociations),
            new { id = associationId },
            associationId);
    }


    /// <summary>
    ///   get all associations as platform admin
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     get a single association as platform admin
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> GetAssociationsForUser(
        Guid userId,
        CancellationToken ct)
    {

        await authorization.EnsureApplicationAdminAsync(ct);

        var associations = await querySender.Send(
            new GetAssociationsForUserQuery(userId),
            ct);

        return Ok(associations);
    }
}