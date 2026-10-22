using Association.Application.DTOs;
using Hivify.Api.Controllers.ControlPlane.Associations.Responses;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Mappers;

public static class AssociationResponseMapper
{
    public static AssociationResponse ToMyResponse(AssociationDto association)
    {
        return new AssociationResponse(
            association.AssociationId,
            association.Name,
            association.ContactEmail,
            association.Identifier,
            association.Status,
            association.CreatedDate
            );
    }



}