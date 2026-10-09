using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Complaints.Application.Contracts;
using Complaints.Domain;

namespace Complaints.Application.Commands.UpdateComplaintStatus;

public sealed class UpdateComplaintStatusCommandHandler
    : ICommandHandler<UpdateComplaintStatusCommand, bool>
{
    private readonly IComplaintRepo _complaintRepository;
    private readonly ICurrentUser _currentUser;


    public UpdateComplaintStatusCommandHandler(
        IComplaintRepo complaintRepository,
        ICurrentUser currentUser
       )
    {
        _complaintRepository = complaintRepository;
        _currentUser = currentUser;

    }

    public async Task<bool> Handle(
        UpdateComplaintStatusCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);


        // Authorization – endast admin
        if (!_currentUser.IsInRole("Admin"))
            throw new UnauthorizedAccessException("Endast administratörer kan ändra status.");

        var complaint = await _complaintRepository.GetComplaintByIdAsync(
            new ComplaintID(command.ComplaintId),
            cancellationToken);

        if (complaint == null)
            throw new KeyNotFoundException($"Complaint with ID {command.ComplaintId} not found.");

        // Uppdatera status
        complaint.UpdateStatus(command.Status);

        // Lägg till admin-kommentar om den finns
        if (!string.IsNullOrWhiteSpace(command.AdminComment))
        {
            complaint.AddAdminComment(command.AdminComment);
        }

        await _complaintRepository.UpdateComplaintAsync(complaint, cancellationToken);

        return true;
    }
}