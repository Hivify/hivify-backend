using SharedKernel;

namespace Complaints.Domain
{
    public readonly record struct DepartmentID(Guid Value) : IValue
    {
    }
}
