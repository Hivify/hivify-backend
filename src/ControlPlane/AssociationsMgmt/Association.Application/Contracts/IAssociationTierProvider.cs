using Association.Application.DTOs;

namespace Association.Application.Contracts;

public interface IAssociationTierProvider
{
    Task<AssociationTierPolicy?> GetPolicyAsync(
        Guid tierId,
        CancellationToken ct = default);
}

