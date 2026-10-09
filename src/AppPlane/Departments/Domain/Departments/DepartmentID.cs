using SharedKernel;

namespace Department.Domain.Departments
{
    public readonly record struct DepartmentID(Guid Value) : IValue
    {
    }
}
