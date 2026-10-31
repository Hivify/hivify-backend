
using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAssociationByIdentifier;

public sealed class GetAssociationByIdentifierQueryHandler(
    IAssociationRepository associationRepository)
    : IQueryHandler<GetAssociationByIdentifierQuery, AssociationDto?>
{
    private readonly IAssociationRepository _associationRepository =
        associationRepository;

    public async Task<AssociationDto?> Handle(
        GetAssociationByIdentifierQuery query,
        CancellationToken ct)
    {
        if (query.Identifier == Guid.Empty)
            return null;

        var association = await _associationRepository.GetByIdentifierAsync(
            new AssociationIdentifier(query.Identifier), ct);

        if (association is null)
            return null;

        return new AssociationDto(
            association.Id.Value,
            association.Name.Value,
            association.ContactEmail.Value,
            association.Identifier.Value,
            association.Status.ToString(),
            association.Members
                .Where(m => m.Role == AssociationRoles.AssociationAdmin)
                .Select(m => m.UserID.Value)
                .FirstOrDefault(),
            association.Members.Count,
            association.CreatedDate
        );
    }
}
