using Complaints.Application.DTOs;
using Hivify.Api.Controllers.AppPlane.Complaints.Responses;

namespace Hivify.Api.Controllers.AppPlane.Complaints.Mappers;

public static class ComplaintResponseMapper
{
    public static CreatedComplaintResponse ToCreatedResponse(
        ComplaintListDto complaint)
    {
        return new CreatedComplaintResponse(
            complaint.Id);
    }

    public static GetComplaintResponse ToGetResponse(
        ComplaintListDto complaint)
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