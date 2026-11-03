using Association.Application.Commands.AcceptMemberInvitation;
using Association.Application.Commands.InviteMember;
using Association.Application.Queries.GetInvitationDetailsQuery;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Hivify.Api.Controllers.AppPlane.Associations.Requests;
using Hivify.Api.Controllers.AppPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.AppPlane.Associations;

[ApiController]
[Route("api/associations/invitations")]
public sealed class AssociationInvitationsController(
    ICommandSender commandSender,
    IQuerySender querySender) : ControllerBase
{
    [Authorize(Policy = "AssociationAdmin")]
    [HttpPost]
    public async Task<ActionResult<Guid>> InviteMember(
        [FromBody] InviteMemberCommand command,
        CancellationToken ct)
    {
        var invitationId = await commandSender.Send(command, ct);

        return Ok(new { InvitationId = invitationId });
    }

    [AllowAnonymous]
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

    [Authorize]
    [HttpPost("accept")]
    public async Task<ActionResult<AcceptInvitationResponse>> AcceptInvitation(
        [FromBody] AcceptInvitationRequest request,
        CancellationToken ct)
    {
        var associationId = await commandSender.Send(
            new AcceptInvitationCommand(request.Token),
            ct);

        return Ok(new AcceptInvitationResponse(associationId));
    }
}
