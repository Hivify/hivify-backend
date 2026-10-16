using Association.Application.Queries.GetUserAssociations;
using BuildingBlocks.ApplicationPorts.Messaging;
using Hivify.Api.Controllers.ControlPlane.Associations.Mappers;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.ControlPlane.Associations;

[ApiController]
[Route("api/associations")]
[Authorize]
public sealed class AssociationsController : ControllerBase
{
    private readonly IQuerySender _querySender;

    public AssociationsController(
        IQuerySender querySender)
    {
        _querySender = querySender;
    }

    [HttpGet("my-association")]
    public async Task<ActionResult<IReadOnlyCollection<MyAssociationResponse>>> GetMyAssociation(
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
}