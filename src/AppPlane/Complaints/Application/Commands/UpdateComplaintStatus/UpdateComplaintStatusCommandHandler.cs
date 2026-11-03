using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using Complaints.Application.Contracts;
using Complaints.Domain;

namespace Complaints.Application.Commands.UpdateComplaintStatus;

public sealed class UpdateComplaintStatusCommandHandler
    : ICommandHandler<UpdateComplaintStatusCommand, bool>
{
    private readonly IComplaintRepo _complaintRepository;
    private readonly ICurrentTenantUser _currentTenantUser;

    public UpdateComplaintStatusCommandHandler(
        IComplaintRepo complaintRepository,
        ICurrentTenantUser currentTenantUser)
    {
        _complaintRepository = complaintRepository;
        _currentTenantUser = currentTenantUser;
    }

    public async Task<bool> Handle(
        UpdateComplaintStatusCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentTenantUser.IsAssociationAdmin)
            throw new UnauthorizedAccessException("Only association administrators can change complaint status.");

        var complaint = await _complaintRepository.GetComplaintByIdAsync(
            new ComplaintID(command.ComplaintId),
            cancellationToken);

        if (complaint == null)
            throw new KeyNotFoundException($"Complaint with ID {command.ComplaintId} not found.");

        complaint.UpdateStatus(command.Status);

        if (!string.IsNullOrWhiteSpace(command.AdminComment))
        {
            complaint.AddAdminComment(command.AdminComment);
        }

        await _complaintRepository.UpdateComplaintAsync(complaint, cancellationToken);

        return true;
    }
}
