using BuildingBlocks.ApplicationPorts.Context.Tenant;
using SharedKernel.ValueObjects;

namespace Association.Application.Context
{
    public sealed class CurrentAssociation(
      ICurrentTenant currentTenant,
      ICurrentTenantUser currentTenantUser) : ICurrentAssociation
    {
        public AssociationID GetAssociationId()
        {
            if (currentTenant.TenantId is not Guid id ||
                id == Guid.Empty)
            {
                throw new InvalidOperationException("No Tenant is resolved for this request.");
            }

            return new AssociationID(id);
        }

        public Guid GetTierId()
        {
            if (currentTenant.TierId is not Guid id ||
                id == Guid.Empty)
            {
                throw new InvalidOperationException(
                    "The current Association does not have a valid tier.");
            }

            return id;
        }

        public void EnsureAuthenticated()
        {
            if (!currentTenantUser.IsAuthenticated ||
                currentTenantUser.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "An authenticated user is required.");
            }
        }

        public void EnsureAdmin()
        {
            if (!currentTenantUser.IsTenantAdmin)
            {
                throw new UnauthorizedAccessException(
                    "Only a Tenant admin can perform this operation.");
            }
        }
    }
}
