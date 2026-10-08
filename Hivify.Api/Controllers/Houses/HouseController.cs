
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Hivify.Api.Controllers.Houses.Requests;
using Houses.Application.Commands.AddTenant;
using Houses.Application.Commands.CreateHouse;
using Houses.Application.Commands.DeleteTenant;
using Houses.Application.Commands.UpdateHouse;
using Houses.Application.Queries.GetHouse;
using Houses.Application.Queries.GetHouses;
using Houses.Application.Queries.GetHouseTenants;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Houses;

[ApiController]
[Route("api/houses")]
public sealed class HousesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public HousesController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }

    // GET: api/houses
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetHousesQuery(),
            cancellationToken);

        return Ok(result);
    }

    // GET: api/houses/{houseId}
    [HttpGet("{houseId:guid}")]
    public async Task<IActionResult> GetById(
        Guid houseId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetHouseQuery(houseId),
            cancellationToken);

        return Ok(result);
    }

    // POST: api/houses
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateHouseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddHouseCommand(
            request.Address,
            request.HouseNumber,
            request.PostalCode);

        var houseId = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { houseId },
            houseId);
    }

    // PUT: api/houses/{houseId}
    [HttpPut("{houseId:guid}")]
    public async Task<IActionResult> Update(
        Guid houseId,
        UpdateHouseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateHouseCommand(
            houseId,
            request.Address,
            request.HouseNumber,
            request.PostalCode);

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // GET: api/houses/{houseId}/tenants
    [HttpGet("{houseId:guid}/tenants")]
    public async Task<IActionResult> GetTenants(
        Guid houseId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetHouseTenantsQuery(houseId),
            cancellationToken);

        return Ok(result);
    }

    // POST: api/houses/{houseId}/tenants
    [HttpPost("{houseId:guid}/tenants")]
    public async Task<IActionResult> AddTenant(
        Guid houseId,
        AddTenantRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddHouseTenantCommand(
            houseId,
            request.UserId,
            request.Email,
            request.FullName,
            request.PhoneNumber);

        var tenantId = await _sender.Send(
            command,
            cancellationToken);

        return Created(
            $"/api/houses/{houseId}/tenants",
            tenantId);
    }

    // DELETE: api/houses/{houseId}/tenants/{tenantId}
    [HttpDelete("{houseId:guid}/tenants/{tenantId:guid}")]
    public async Task<IActionResult> RemoveTenant(
        Guid houseId,
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new RemoveHouseTenantCommand(
                houseId,
                tenantId),
            cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}






