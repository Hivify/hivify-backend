using Association.Application.Queries.GetInvitationDetailsQuery;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Hivify.Api.Controllers.ControlPlane.Associations.Requests;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/invitations")]
[AllowAnonymous]
public sealed class PublicInvitationController(
    IQuerySender querySender) : ControllerBase
{
    [HttpPost("details")]
    public async Task<ActionResult<InvitationDetailsResponse>> GetInvitationDetails(
        [FromBody] GetInvitationDetailsRequest request,
        CancellationToken ct)
    {
        var invitation = await querySender.Send(
            new GetInvitationDetailsQuery(request.Token),
            ct);

        if (invitation is null)
            return NotFound();

        var response = new InvitationDetailsResponse(
            invitation.InvitationId,
            invitation.AssociationName,
            invitation.InvitedByUserName,
            invitation.InvitedByUserEmail,
            invitation.InvitationSentAt,
            invitation.InvitationExpiresAt,
            invitation.IsValid);

        return Ok(response);
    }
}

