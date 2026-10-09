using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Department.Application.Contracts;
using Department.Domain.Departments;
using Department.Domain.Members;
using SharedKernel.ValueObjects;

namespace Department.Application.Commands.AddStaffMember;

public sealed class AddStaffMemberCommandHandler : ICommandHandler<AddStaffMemberCommand, MemberID>
{
    private readonly IDepartmentRepo _departmentRepository;

    public AddStaffMemberCommandHandler(IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<MemberID> Handle(AddStaffMemberCommand command, CancellationToken cancellationToken)
    {
        var departmentEntity = await _departmentRepository.GetByIdAsync(new DepartmentID(command.DepartmentId), cancellationToken);

        if (departmentEntity is null)
            throw new InvalidOperationException("DepartmentEntity was not found.");

        var member = departmentEntity.CreateMember(new UserID(command.UserId), new Name(command.FullName), new Email(command.Email), command.Role);

        await _departmentRepository.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}
