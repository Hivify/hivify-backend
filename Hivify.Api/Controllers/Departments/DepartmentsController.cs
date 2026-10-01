using Department.Application.Commands.AddDepartment;
using Department.Application.Commands.RemoveStaffMember;
using Department.Application.Queries.GetDepartment.AllDepartments;
using Department.Application.Queries.GetDepartment.SingleDepartment;
using Department.Application.Queries.GetMember.AllMembers;
using Department.Application.Queries.GetMember.SingleMember;
using BuildingBlocks.ApplicationPorts.Messaging;
using Hivify.Api.Controllers.Departments.Requests;
using Hivify.Api.Controllers.Departments.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Departments;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/departments")]
public sealed class DepartmentsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public DepartmentsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }


    [HttpGet]
    public async Task<IActionResult> GetDepartments(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetDepartmentsQuery(),
            cancellationToken);

        return Ok(result);
    }




    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDepartment(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetDepartmentQuery(id),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }




    [HttpPost]
    public async Task<ActionResult<CreatedDepartmentRes>> CreateDepartment(CreateDepartmentReq request, CancellationToken cancellationToken)
    {
        var command = new AddDepartmentCommand(request.Name);
        var id = await _sender.Send(command, cancellationToken);
        var response = new CreatedDepartmentRes(id.Value);
        return CreatedAtAction(
            nameof(GetDepartment),
            new { id = id.Value },
            response);
    }




    // memeber management endpoints


    [HttpGet("{departmentId:guid}/members/{memberId:guid}")]
    public async Task<IActionResult> GetMember(
        Guid departmentId,
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetSingleMemberQuery(departmentId, memberId),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }




    [HttpPost("{departmentId:guid}/members")]
    public async Task<ActionResult<CreatedDepartmentMemberRes>> AddMember(Guid departmentId,
       CreateDepartmentMemberReq request,
       CancellationToken cancellationToken)
    {
        var command = new AddStaffMemberCommand(
            departmentId,
            request.UserId,
            request.FullName,
            request.Email,
            request.Role);

        var memberId = await _sender.Send(
            command,
            cancellationToken);

        var response = new CreatedDepartmentMemberRes(memberId.Value);

        return CreatedAtAction(
        nameof(GetMember),
        new
        {
            departmentId,
            memberId = memberId.Value
        },
        response);
    }


    [HttpGet("{departmentId:guid}/members")]
    public async Task<IActionResult> GetMembers(
        Guid departmentId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetMembersQuery(departmentId),
            cancellationToken);
        return Ok(result);
    }




    [HttpDelete("{departmentId:guid}/members/{memberId:guid}")]
    public async Task<IActionResult> RemoveMember(
        Guid departmentId,
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new RemoveStaffMemberCommand(
                departmentId,
                memberId),
            cancellationToken);

        return result
            ? NoContent()
            : NotFound();
    }



}
