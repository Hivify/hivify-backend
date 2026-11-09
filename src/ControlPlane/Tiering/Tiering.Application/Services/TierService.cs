
using Tiering.Application.Contracts;
using Tiering.Application.DTOs;
using Tiering.Domain.Enums;
using Tiering.Domain.ValueObjcts;

namespace Tiering.Application.Services;

public sealed class TierService(ITierRepository tierRepository) : ITierService
{
    public async Task<IReadOnlyList<TierDto>> GetAllTiersAsync(
        CancellationToken ct = default)
    {
        var tiers = await tierRepository.GetAllAsync(ct);

        return tiers.Select(tier => new TierDto(
            tier.Id.Value,
            tier.TierName.Value,
            tier.MaxMembers.Value,
            tier.MaxAdministrators.Value,
            tier.Status.ToString()
        )).ToList();
    }

    public async Task<bool> IsTierAvailableAsync(
        Guid tierId,
        CancellationToken ct = default)
    {
        var tier = await tierRepository.GetByIdAsync(
            new TierID(tierId),
            ct);

        return tier is not null && tier.Status == TierStatus.Active;
    }
}
