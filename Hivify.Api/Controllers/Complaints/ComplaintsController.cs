
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Complaints.Application.Commands.CreateComplaint;
using Complaints.Application.Commands.UpdateComplaintStatus;
using Complaints.Application.Queries.GetComplaint;
using Hivify.Api.Controllers.Complaints.Mappers;
using Hivify.Api.Controllers.Complaints.Requests;
using Hivify.Api.Controllers.Complaints.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Complaints;

[ApiController]
[Route("api/complaints")]
public sealed class ComplaintsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public ComplaintsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IReadOnlyCollection<GetComplaintResponse>>> GetComplaints(
        CancellationToken cancellationToken)
    {
        var complaints = await _querySender.Send(
            new GetAllComplaintsQuery(),
            cancellationToken);

        var response = complaints
            .Select(ComplaintResponseMapper.ToGetResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetComplaintResponse>> GetComplaint(
        Guid id,
        CancellationToken cancellationToken)
    {
        var complaint = await _querySender.Send(
            new GetComplaintByIdQuery(id),
            cancellationToken);

        if (complaint is null)
            return NotFound();

        return Ok(ComplaintResponseMapper.ToGetResponse(complaint));
    }

    [HttpPost]
    public async Task<ActionResult<CreatedComplaintResponse>> CreateComplaint(
       CreateComplaintRequest request,
       CancellationToken cancellationToken)
    {
        var command = new CreateComplaintCommand(
            request.AssociationId,
            request.Title,
            request.Description,
            request.ImageUrl);

        var id = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetComplaint),
            new { id },
            new CreatedComplaintResponse(id));
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateComplaintStatus(
          Guid id,
          UpdateComplaintStatusRequest request,
          CancellationToken cancellationToken)
    {
        var command = new UpdateComplaintStatusCommand(
            id,
            request.ToDomainStatus(),
            request.AdminComment);

        var result = await _sender.Send(
            command,
            cancellationToken);

        return result
            ? NoContent()
            : NotFound();
    }
}

