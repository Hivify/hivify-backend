using Association.Application.DTOs;

namespace Association.Application.Contracts;

public interface IAssociationTierPort
{
    Task<AssociationTierPolicy?> GetPolicyAsync(
        Guid tierId,
        CancellationToken ct = default);
}

