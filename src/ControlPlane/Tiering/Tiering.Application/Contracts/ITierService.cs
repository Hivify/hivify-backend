using Tiering.Application.DTOs;

namespace Tiering.Application.Contracts;

public interface ITierService
{
    Task<IReadOnlyList<TierDto>> GetAllTiersAsync(CancellationToken ct = default);
    Task<bool> IsTierAvailableAsync(Guid tierId, CancellationToken ct = default);
    Task<TierPolicyDto?> GetTierPolicyAsync(Guid tierId, CancellationToken ct = default);


}