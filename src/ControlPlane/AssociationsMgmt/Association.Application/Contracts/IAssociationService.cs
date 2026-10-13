namespace Association.Application.Contracts;

public interface IAssociationService
{
    Task<Guid> CreateAssociationAsync(string name, CancellationToken ct = default);
}