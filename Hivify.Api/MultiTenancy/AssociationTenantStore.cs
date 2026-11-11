using Association.Application.DTOs;
using Association.Application.Queries.GetAssociationByIdentifier;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Finbuckle.MultiTenant.Abstractions;

namespace Hivify.Api.MultiTenancy;

public sealed class AssociationTenantStore(IQueryHandler<GetAssociationByIdentifierQuery, AssociationDto?> queryHandler) : IMultiTenantStore<ExtendedTenantInfo>
{
    public async Task<ExtendedTenantInfo?> GetByIdentifierAsync(string identifier)
    {
        if (!Guid.TryParse(identifier, out var associationIdentifier) ||
            associationIdentifier == Guid.Empty)
        {
            return null;
        }

        var query = new GetAssociationByIdentifierQuery(associationIdentifier);

        var association = await queryHandler.Handle(
            query,
            CancellationToken.None);

        if (association is null)
            return null;

        return new ExtendedTenantInfo
        {
            Id = association.AssociationId.ToString(),
            TierId = association.TierId.ToString(),
            Identifier = association.Identifier.ToString(),
            Name = association.Name,
        };
    }

    public Task<bool> AddAsync(ExtendedTenantInfo tenantInfo)
        => throw new NotSupportedException();

    public Task<IEnumerable<ExtendedTenantInfo>> GetAllAsync()
        => throw new NotSupportedException();

    public Task<IEnumerable<ExtendedTenantInfo>> GetAllAsync(int take, int skip)
        => throw new NotSupportedException();

    public Task<ExtendedTenantInfo?> GetAsync(string id)
        => throw new NotSupportedException();

    public Task<bool> RemoveAsync(string identifier)
        => throw new NotSupportedException();

    public Task<bool> UpdateAsync(ExtendedTenantInfo tenantInfo)
        => throw new NotSupportedException();
}