using Complaints.Application.DTOs;
using Hivify.Api.Controllers.AppPlane.Complaints.Responses;

namespace Hivify.Api.Controllers.AppPlane.Complaints.Mappers;

public static class ComplaintResponseMapper
{
    public static CreatedComplaintResponse ToCreatedResponse(
        ComplaintListItem complaint)
    {
        return new CreatedComplaintResponse(
            complaint.Id);
    }

    public static GetComplaintResponse ToGetResponse(
        ComplaintListItem complaint)
    {
        return new GetComplaintResponse(
            complaint.Id,
            complaint.AssociationId,
            complaint.Title,
            complaint.Description,
            complaint.Status.ToString(),
            complaint.AdminComment,
            complaint.ImageUrl,
            complaint.CreatedDate);
    }
}