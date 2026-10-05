using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.Contracts;
using Department.Domain.Departments;
using SharedKernel.ValuesObjects;

namespace Department.Application.Commands.AddDepartment;

public sealed class CreateDepartmentCommandHandler : ICommandHandler<AddDepartmentCommand, DepartmentID>
{
    private readonly IDepartmentRepo _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentID> Handle(AddDepartmentCommand command, CancellationToken cancellationToken)
    {
        var departmentEntity = DepartmentEntity.Create(new Name(command.Name));

        await _departmentRepository.AddAsync(departmentEntity, cancellationToken);

        await _departmentRepository.SaveChangesAsync(cancellationToken);

        return departmentEntity.Id;
    }
}
