using Association.Application.DTOs;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Mappers;

public static class AssociationResponseMapper
{
    public static AssociationResponse ToMyResponse(TenantAssociationOutput association)
    {
        return new AssociationResponse(
            association.AssociationId,
            association.AssociationName,
            association.AdminAssociationEmail,
            association.Identifier,
            association.Status,
            association.CreatedDate
            );
    }



}