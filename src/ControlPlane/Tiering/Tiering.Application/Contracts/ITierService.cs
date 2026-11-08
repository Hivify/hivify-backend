using Tiering.Application.DTOs;

namespace Tiering.Application.Contracts;

public interface ITierService
{
    Task<IReadOnlyList<TierDto>> GetAllTiersAsync(CancellationToken ct = default);
}