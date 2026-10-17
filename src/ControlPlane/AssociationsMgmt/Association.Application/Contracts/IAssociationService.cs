using Association.Application.DTOs;

namespace Association.Application.Contracts;

public interface IAssociationService
{
    Task<Guid> CreateAssociationAsync(string name, CancellationToken ct = default);
    Task<TenantAssociationOutput?> GetByIdentifierAsync(
    Guid identifier,
    CancellationToken cancellationToken = default);
}