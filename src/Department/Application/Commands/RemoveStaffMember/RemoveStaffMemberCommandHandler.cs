using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.Contracts;
using Department.Domain.Departments;

namespace Department.Application.Commands.RemoveStaffMember;

public sealed class RemoveStaffMemberCommandHandler
    : ICommandHandler<RemoveStaffMemberCommand, bool>
{
    private readonly IDepartmentRepo _departmentRepository;

    public RemoveStaffMemberCommandHandler(
        IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(
        RemoveStaffMemberCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var departmentEntity = await _departmentRepository.GetByIdAsync(
            new DepartmentID(command.DepartmentId),
            cancellationToken);

        if (departmentEntity is null)
        {
            throw new KeyNotFoundException(
                $"DepartmentEntity {command.DepartmentId} was not found.");
        }

        var member = departmentEntity.StaffMembers
            .FirstOrDefault(member =>
                member.Id.Value == command.MemberId &&
                member.DeletedAt == null);

        if (member is null)
        {
            throw new KeyNotFoundException(
                $"Member {command.MemberId} was not found.");
        }

        departmentEntity.RemoveMember(member);

        await _departmentRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}
