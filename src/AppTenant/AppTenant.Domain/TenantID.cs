using SharedKernel;

namespace AppTenant.Domain
{
    public readonly record struct TenantID(Guid Value) : IValue
    {
    }
}



