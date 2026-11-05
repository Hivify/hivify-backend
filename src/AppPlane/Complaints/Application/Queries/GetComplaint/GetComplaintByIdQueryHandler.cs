using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Complaints.Application.Contracts;
using Complaints.Application.DTOs;
using Complaints.Domain;

namespace Complaints.Application.Queries.GetComplaint;

public sealed class GetComplaintByIdQueryHandler
    : IQueryHandler<GetComplaintByIdQuery, ComplaintListDto?>
{
    private readonly IComplaintRepo _complaintRepository;

    public GetComplaintByIdQueryHandler(IComplaintRepo complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }

    public async Task<ComplaintListDto?> Handle(
        GetComplaintByIdQuery query,
        CancellationToken cancellationToken)
    {
        var complaint = await _complaintRepository.GetComplaintByIdAsync(
            new ComplaintID(query.ComplaintId),
            cancellationToken);

        if (complaint == null)
            return null;

        return new ComplaintListDto(
            complaint.Id.Value,
            complaint.DepartmentId.Value,
            complaint.Title.Value,
            complaint.Description.Value,
            complaint.Status,
            complaint.CreatedDate,
            complaint.ImageUrl,
            complaint.AdminComment,
            complaint.UserId.Value.ToString()
        );
    }
}