using Association.Application.Contracts;
using Association.Application.Queries.GetUserAssociations;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Hivify.Api.Controllers.ControlPlane.Associations.Mappers;
using Hivify.Api.Controllers.ControlPlane.Associations.Requests;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/associations")]
[Authorize]
public sealed class AssociationsController(
    IQuerySender querySender,
    IAssociationService associationService) : ControllerBase
{
    private readonly IAssociationService _associationService = associationService;
    private readonly IQuerySender _querySender = querySender;

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

        var id = await _associationService.CreateAssociationAsync(request.Name, cancellationToken);
        return CreatedAtAction(
            nameof(GetMyAssociations),
            new { id },
            id);
    }
}