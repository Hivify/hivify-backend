using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using Complaints.Application.Contracts;
using Complaints.Domain;
using SharedKernel.ValueObjects;

namespace Complaints.Application.Commands.CreateComplaint;

public sealed class CreateComplaintCommandHandler(
    IComplaintRepo complaintRepository,
    ICurrentTenantUser currentTenantUser
    ) : ICommandHandler<CreateComplaintCommand, Guid>
{
    private readonly IComplaintRepo _complaintRepository = complaintRepository;
    private readonly ICurrentTenantUser _currentTenantUser = currentTenantUser;

    public async Task<Guid> Handle(CreateComplaintCommand command, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentTenantUser.IsAuthenticated || _currentTenantUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = _currentTenantUser.UserId;

        var complaint = Complaint.Create(
            new UserID(userId),
            new DepartmentID(command.AssociationId),
            new Title(command.Title),
            new Description(command.Description),
            command.ImageUrl);

        await _complaintRepository.CreateComplaintAsync(complaint, ct);

        return complaint.Id.Value;
    }
}