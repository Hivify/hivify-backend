using Association.Application.DTOs;
using Hivify.Api.Controllers.Associations.Responses;

namespace Hivify.Api.Controllers.Associations.Mappers;

public static class AssociationResponseMapper
{
    public static MyAssociationResponse ToMyResponse(TenantAssociationOutput association)
    {
        return new MyAssociationResponse(
            association.AssociationId,
            association.Name,
            association.Identifier,
            association.Role);
    }
}