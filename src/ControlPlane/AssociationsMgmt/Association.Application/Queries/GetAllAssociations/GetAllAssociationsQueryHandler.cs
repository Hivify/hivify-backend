using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Authorization;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAllAssociations;

public sealed class GetAllAssociationsQueryHandler(IAssociationRepository associationRepository, IApplicationAuthorization authorization)
    : IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly IApplicationAuthorization _authorization = authorization;

    public async Task<IReadOnlyList<TenantAssociationOutput>> Handle(GetAllAssociationsQuery query, CancellationToken ct)
    {
        await _authorization.EnsureApplicationAdminAsync(ct);

        var associations = await _associationRepository.GetAllAsync(ct);

        return associations
            .Select(a => new TenantAssociationOutput(
                    a.Id.Value,
                    a.Name.Value,
                    a.ContactEmail.Value,
                    a.Identifier.Value,
                    a.Status.ToString(),
                    a.CreatedDate
              ))
            .ToList();
    }
}
