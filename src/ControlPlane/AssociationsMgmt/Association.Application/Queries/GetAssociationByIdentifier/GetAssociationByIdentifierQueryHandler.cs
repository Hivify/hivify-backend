using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociationByIdentifier;

public sealed class GetAssociationByIdentifierQueryHandler(IAssociationRepository associationRepository)
    : IQueryHandler<GetAssociationByIdentifierQuery, TenantAssociationOutput?>
{
    private readonly IAssociationRepository _associationRepository = associationRepository;

    public async Task<TenantAssociationOutput?> Handle(GetAssociationByIdentifierQuery query, CancellationToken ct)
    {
        if (query.Identifier == Guid.Empty)
            return null;

        var association = await _associationRepository.GetByIdentifierAsync(new AssociationIdentifier(query.Identifier), ct);

        if (association is null)
            return null;

        return new TenantAssociationOutput(
            association.Id.Value,
            association.Name.Value,
            association.Identifier.Value,
            association.Status.ToString(),
            association.CreatedDate);
    }
}