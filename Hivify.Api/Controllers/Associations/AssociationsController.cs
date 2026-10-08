using Association.Application.Commands.CreateAssociation;
using Association.Application.Queries.GetUserAssociations;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Hivify.Api.Controllers.Associations.Mappers;
using Hivify.Api.Controllers.Associations.Requests;
using Hivify.Api.Controllers.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Associations;

[ApiController]
[Route("api/associations")]
[Authorize]
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

    [HttpGet("my")]
    public async Task<ActionResult<IReadOnlyCollection<MyAssociationResponse>>> GetMyAssociations(
        CancellationToken cancellationToken)
    {
        var associations = await _querySender.Send(
            new GetUserAssociationsQuery(),
            cancellationToken);

        var response = associations
            .Select(AssociationResponseMapper.ToMyResponse)
            .ToList();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAssociation(
        CreateAssociationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAssociationCommand(request.Name);

        var id = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetMyAssociations),
            new { id },
            id);
    }
}