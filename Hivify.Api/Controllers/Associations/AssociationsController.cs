using Association.Application.Commands.AddAssociation;
using Association.Application.Commands.RemoveStaffMember;
using Association.Application.Queries.GetAssociation.AllAssociations;
using Association.Application.Queries.GetAssociation.SingleAssociation;
using Association.Application.Queries.GetMember.AllMembers;
using Association.Application.Queries.GetMember.SingleMember;
using BuildingBlocks.ApplicationPorts.Messaging;
using Hivify.Api.Controllers.Associations.Requests;
using Hivify.Api.Controllers.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Associations;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/associations")]
public sealed class AssociationsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public AssociationsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }


    [HttpGet]
    public async Task<IActionResult> GetAssociations(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetAssociationsQuery(),
            cancellationToken);

        return Ok(result);
    }



    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAssociation(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetAssociationQuery(id),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }





    [HttpPost]
    public async Task<ActionResult<CreatedAssociationRes>> CreateAssociation(CreateAssociationReq request, CancellationToken cancellationToken)
    {
        var command = new AddAssociationCommand(request.Name);
        var id = await _sender.Send(command, cancellationToken);
        var response = new CreatedAssociationRes(id.Value);
        return CreatedAtAction(
            nameof(GetAssociation),
            new { id = id.Value },
            response);
    }





    // memeber management endpoints


    [HttpGet("{associationId:guid}/members/{memberId:guid}")]
    public async Task<IActionResult> GetMember(
        Guid associationId,
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetSingleMemberQuery(associationId, memberId),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }





    [HttpPost("{associationId:guid}/members")]
    public async Task<ActionResult<CreatedAssociationMemberRes>> AddMember(Guid associationId,
       CreateAssociationMemberReq request,
       CancellationToken cancellationToken)
    {
        var command = new AddStaffMemberCommand(
            associationId,
            request.UserId,
            request.FullName,
            request.Email,
            request.Role);

        var memberId = await _sender.Send(
            command,
            cancellationToken);

        var response = new CreatedAssociationMemberRes(memberId.Value);

        return CreatedAtAction(
        nameof(GetMember),
        new
        {
            associationId,
            memberId = memberId.Value
        },
        response);
    }


    [HttpGet("{associationId:guid}/members")]
    public async Task<IActionResult> GetMembers(
        Guid associationId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetMembersQuery(associationId),
            cancellationToken);
        return Ok(result);
    }




    [HttpDelete("{associationId:guid}/members/{memberId:guid}")]
    public async Task<IActionResult> RemoveMember(
        Guid associationId,
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new RemoveStaffMemberCommand(
                associationId,
                memberId),
            cancellationToken);

        return result
            ? NoContent()
            : NotFound();
    }



}