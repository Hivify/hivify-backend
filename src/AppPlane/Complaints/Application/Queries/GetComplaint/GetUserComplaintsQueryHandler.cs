using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using Complaints.Application.Contracts;
using Complaints.Application.DTOs;
using SharedKernel.ValueObjects;

namespace Complaints.Application.Queries.GetComplaint;

public sealed class GetUserComplaintsQueryHandler
    : IQueryHandler<GetUserComplaintsQuery, IReadOnlyList<ComplaintListItem>>
{
    private readonly IComplaintRepo _complaintRepository;
    private readonly ICurrentTenantUser _currentTenantUser;

    public GetUserComplaintsQueryHandler(IComplaintRepo complaintRepository, ICurrentTenantUser currentTenantUser)
    {
        _complaintRepository = complaintRepository;
        _currentTenantUser = currentTenantUser;
    }

    public async Task<IReadOnlyList<ComplaintListItem>> Handle(
        GetUserComplaintsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _currentTenantUser.UserId;

        var complaints = await _complaintRepository.GetComplaintsByUserAsync(
            new UserID(userId),
            cancellationToken);

        return complaints
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new ComplaintListItem(
                c.Id.Value,
                c.DepartmentId.Value,
                c.Title.Value,
                c.Description.Value,
                c.Status,
                c.CreatedDate,
                c.ImageUrl,
                c.AdminComment))
            .ToList();
    }
}