using Association.Application.DTOs;
using Association.Application.Queries.GetAssociationByIdentifier;
using BuildingBlocks.ApplicationPorts.Messaging;
using Finbuckle.MultiTenant.Abstractions;

namespace Hivify.Api.MultiTenancy;

public sealed class AssociationTenantStore(IQueryHandler<GetAssociationByIdentifierQuery, AssociationDto?> queryHandler)
    : IMultiTenantStore<TenantInfo>
{
    public async Task<TenantInfo?> GetByIdentifierAsync(string identifier)
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

        return new TenantInfo
        {
            Id = association.AssociationId.ToString(),
            Identifier = association.Identifier.ToString(),
            Name = association.Name,
        };
    }

    public Task<bool> AddAsync(TenantInfo tenantInfo)
        => throw new NotSupportedException();

    public Task<IEnumerable<TenantInfo>> GetAllAsync()
        => throw new NotSupportedException();

    public Task<IEnumerable<TenantInfo>> GetAllAsync(int take, int skip)
        => throw new NotSupportedException();

    public Task<TenantInfo?> GetAsync(string id)
        => throw new NotSupportedException();

    public Task<bool> RemoveAsync(string identifier)
        => throw new NotSupportedException();

    public Task<bool> UpdateAsync(TenantInfo tenantInfo)
        => throw new NotSupportedException();
}