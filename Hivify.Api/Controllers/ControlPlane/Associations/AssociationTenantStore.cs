using Association.Application.Contracts;
using Finbuckle.MultiTenant.Abstractions;

namespace Hivify.Api.Controllers.ControlPlane.Associations
{
    public sealed class AssociationTenantStore(
    IAssociationService associationService)
    : IMultiTenantStore<TenantInfo>
    {
        public async Task<TenantInfo?> GetByIdentifierAsync(string identifier)
        {
            if (!Guid.TryParse(identifier, out var associationIdentifier) ||
                associationIdentifier == Guid.Empty)
            {
                return null;
            }

            var association = await associationService.GetByIdentifierAsync(
                associationIdentifier);

            if (association is null)
                return null;

            return new TenantInfo
            {
                Id = association.AssociationId.ToString(),
                Identifier = association.Identifier.ToString(),
                Name = association.Name
            };
        }
        public Task<bool> AddAsync(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TenantInfo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TenantInfo>> GetAllAsync(int take, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<TenantInfo?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }



        public Task<bool> RemoveAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }
    }
}
