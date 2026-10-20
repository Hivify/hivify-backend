using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CurrentTenantPort;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetAssociationDetailsById;

public sealed class GetCurrentAssociationDetailsQueryHandler(
    IAssociationRepository associationRepository,
    ICurrentTenant currentTenant)
    : IQueryHandler<GetCurrentAssociationDetailsQuery, AssociationDetailsOutput?>
{
    public async Task<AssociationDetailsOutput?> Handle(
        GetCurrentAssociationDetailsQuery query,
        CancellationToken ct)
    {
        var association = await associationRepository.GetDetailsByIdAsync(
            new AssociationID(currentTenant.AssociationId),
            ct);

        if (association is null)
            return null;

        return new AssociationDetailsOutput(
            association.AssociationId,
            association.Name,
            association.Identifier,
            association.Status.ToString(),
            association.CreatedDate,
            association.OwnerUserId,
            association.OwnerEmail,
            association.MemberCount);
    }
}