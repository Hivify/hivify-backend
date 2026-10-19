using Association.Application.DTOs;

namespace Association.Application.Contracts;

public interface IAssociationService
{
    Task<Guid> CreateAssociationAsync(string name, CancellationToken ct = default);
    Task<TenantAssociationOutput?> GetAssociationByIdentifierAsync(Guid identifier, CancellationToken ct = default);
}