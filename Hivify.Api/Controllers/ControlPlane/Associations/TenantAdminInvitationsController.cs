using Association.Application.Commands.InviteMember;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/tenant-admin/invitations")]
[Authorize(Policy = "TenantAdmin")]
public sealed class TenantAdminInvitationsController(ISender commandSender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> InviteMember([FromBody] InviteMemberCommand command,
        CancellationToken ct)
    {
        var invitationId = await commandSender.Send(command, ct);

        return Ok(new { InvitationId = invitationId });
    }
}