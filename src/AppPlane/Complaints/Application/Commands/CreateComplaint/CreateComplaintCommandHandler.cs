using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using Complaints.Application.Contracts;
using Complaints.Domain;
using SharedKernel.ValueObjects;

namespace Complaints.Application.Commands.CreateComplaint;

public sealed class CreateComplaintCommandHandler : ICommandHandler<CreateComplaintCommand, Guid>
{
    private readonly IComplaintRepo _complaintRepository;
    private readonly ICurrentUser _currentUser;

    public CreateComplaintCommandHandler(
        IComplaintRepo complaintRepository,
        ICurrentUser currentUser
    )
    {
        _complaintRepository = complaintRepository;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateComplaintCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentUser.IsAuthenticated || _currentUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = _currentUser.UserId;

        var complaint = Complaint.Create(
            new UserID(userId),
            new DepartmentID(command.AssociationId),
            new Title(command.Title),
            new Description(command.Description),
            command.ImageUrl);

        await _complaintRepository.CreateComplaintAsync(complaint, cancellationToken);

        return complaint.Id.Value;
    }
}