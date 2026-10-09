using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Complaints.Domain;

namespace Complaints.Application.Commands.UpdateComplaintStatus;

public sealed record UpdateComplaintStatusCommand(
    Guid ComplaintId,
    ComplaintStatus Status,
    string? AdminComment
) : ICommand<bool>;