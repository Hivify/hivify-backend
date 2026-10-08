using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using Complaints.Application.Contracts;
using Complaints.Application.DTOs;
using SharedKernel.ValueObjects;

namespace Complaints.Application.Queries.GetComplaint;

public sealed class GetUserComplaintsQueryHandler
    : IQueryHandler<GetUserComplaintsQuery, IReadOnlyList<ComplaintListItem>>
{
    private readonly IComplaintRepo _complaintRepository;
    private readonly ICurrentUser _currentUser;

    public GetUserComplaintsQueryHandler(IComplaintRepo complaintRepository, ICurrentUser currentUser)
    {
        _complaintRepository = complaintRepository;
        _currentUser = currentUser;
    }

    public async Task<IReadOnlyList<ComplaintListItem>> Handle(
        GetUserComplaintsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var complaints = await _complaintRepository.GetComplaintsByUserAsync(
            new UserID(userId),
            cancellationToken);

        return complaints
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new ComplaintListItem(
                c.Id.Value,
                c.AssociationId.Value,
                c.Title.Value,
                c.Description.Value,
                c.Status,
                c.CreatedDate,
                c.ImageUrl,
                c.AdminComment))
            .ToList();
    }
}