namespace Association.Application.Contracts;

public interface IAssociationService
{
    Task<Guid> CreateAssociationAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> HasAssociationsAsync(CancellationToken cancellationToken = default);
}