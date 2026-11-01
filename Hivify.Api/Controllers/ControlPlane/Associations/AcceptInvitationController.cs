using Association.Application.Commands.AcceptInvitation;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Hivify.Api.Controllers.ControlPlane.Associations.Requests;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/associations/invitations")]
[Authorize]
public sealed class AcceptInvitationController(
    ISender commandSender) : ControllerBase
{
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



