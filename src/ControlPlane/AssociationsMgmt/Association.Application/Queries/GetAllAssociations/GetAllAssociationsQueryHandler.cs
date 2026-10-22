
using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.Enums;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAllAssociations;

public sealed class GetAllAssociationsQueryHandler(
    IAssociationRepository associationRepository)
    : IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<AssociationDto>>
{
    private readonly IAssociationRepository _associationRepository =
        associationRepository;

    public async Task<IReadOnlyList<AssociationDto>> Handle(
        GetAllAssociationsQuery query,
        CancellationToken ct)
    {
        var associations =
            await _associationRepository.GetAllAsync(ct);

        return associations
            .Select(a => new AssociationDto(
                a.Id.Value,
                a.Name.Value,
                a.ContactEmail.Value,
                a.Identifier.Value,
                a.Status.ToString(),
                a.Members
                    .Where(m => m.Role == AssociationRoles.TenantAdmin)
                    .Select(m => m.UserID.Value)
                    .FirstOrDefault(),
                a.Members.Count,
                a.CreatedDate
            ))
            .ToList();
    }
}
