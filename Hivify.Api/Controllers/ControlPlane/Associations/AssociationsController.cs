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
    public async Task<ActionResult<IReadOnlyCollection<AssociationResponse>>> GetMyAssociation(CancellationToken ct)
    {
        var associations = await _querySender.Send(
            new GetUserAssociationsQuery(),
            ct);

        var response = associations
            .Select(AssociationResponseMapper.ToMyResponse)
            .ToList();

        return Ok(response);
    }






}