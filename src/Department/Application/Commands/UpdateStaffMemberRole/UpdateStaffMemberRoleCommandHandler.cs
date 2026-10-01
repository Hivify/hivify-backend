using Department.Application.Contracts;
using Department.Domain.Departments;
using Department.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Commands.UpdateStaffMemberRole;

public sealed class UpdateStaffMemberRoleCommandHandler
    : ICommandHandler<UpdateStaffMemberRoleCommand, bool>
{
    private readonly IDepartmentRepo _departmentRepository;

    public UpdateStaffMemberRoleCommandHandler(
        IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(
        UpdateStaffMemberRoleCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var departmentEntity =
            await _departmentRepository.GetByIdAsync(
                new DepartmentID(command.DepartmentId),
                cancellationToken);

        if (departmentEntity is null)
        {
            throw new KeyNotFoundException(
                $"DepartmentEntity {command.DepartmentId} was not found.");
        }

        departmentEntity.UpdateMemberRole(
            new MemberID(command.MemberId),
            command.Role);

        await _departmentRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}
