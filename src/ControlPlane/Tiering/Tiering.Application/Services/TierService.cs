using Tiering.Application.Contracts;
using Tiering.Application.DTOs;

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


}