using Complaints.Domain;
using Hivify.Api.Controllers.AppPlane.Complaints.Requests;

namespace Hivify.Api.Controllers.AppPlane.Complaints.Mappers;

public static class UpdateComplaintStatusMapper
{
    public static ComplaintStatus ToDomainStatus(
        this UpdateComplaintStatusRequest request)
    {
        return request.Status switch
        {
            "Ny" => ComplaintStatus.New,
            "Under behandling" => ComplaintStatus.UnderTreatment,
            "Löst" => ComplaintStatus.Resolved,

            _ => throw new ArgumentException(
                $"Unknown complaint status: '{request.Status}'.",
                nameof(request.Status))
        };
    }
}