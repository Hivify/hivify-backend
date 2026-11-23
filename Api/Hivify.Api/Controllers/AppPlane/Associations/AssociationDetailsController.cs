using Association.Application.DTOs;
using Association.Application.Queries.GetAssociationDetailsById;
using Association.Application.Queries.GetAssociationMembers;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Hivify.Api.Controllers.AppPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.AppPlane.Associations;

[ApiController]
[Route("api/associations")]
[Authorize(Policy = "AssociationAdmin")]
public sealed class AssociationDetailsController(IQuerySender querySender) : ControllerBase
{


    /// <summary>
    ///  Get the current Association 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
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


    /// <summary>
    ///      Get the members of the current association.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>


    [HttpGet("members")]
    public async Task<ActionResult<IReadOnlyList<AssociationMemberResponse>>> GetAssociationMembers(
     CancellationToken ct)
    {
        var members = await querySender.Send(
            new GetAssociationMembersQuery(),
            ct);

        var response = members
            .Select(member => new AssociationMemberResponse(
                member.UserId,
                member.Role,
                member.JoinedAt))
            .ToList();

        return Ok(response);
    }
}
