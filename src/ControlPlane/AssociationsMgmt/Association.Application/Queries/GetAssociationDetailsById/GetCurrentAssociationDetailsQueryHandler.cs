
using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CurrentTenantPort;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetAssociationDetailsById;

public sealed class GetCurrentAssociationDetailsQueryHandler(
    IAssociationRepository associationRepository,
    ICurrentTenant currentTenant)
    : IQueryHandler<GetCurrentAssociationDetailsQuery, AssociationDto?>
{
    public async Task<AssociationDto?> Handle(
        GetCurrentAssociationDetailsQuery query,
        CancellationToken ct)
    {
        var associationId = currentTenant.AssociationId;

        if (associationId is null)
            return null;

        var association = await associationRepository.GetDetailsByIdAsync(
            new AssociationID(associationId.Value),
            ct);

        if (association is null)
            return null;

        return new AssociationDto(
            association.AssociationId,
            association.Name,
            association.ContactEmail,
            association.Identifier,
            association.Status,
            association.OwnerUserId,
            association.MemberCount,
            association.CreatedDate);
    }
}
