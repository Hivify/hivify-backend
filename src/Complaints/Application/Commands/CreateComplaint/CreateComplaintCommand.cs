using BuildingBlocks.ApplicationPorts.Messaging;

namespace Complaints.Application.Commands.CreateComplaint
{
    public sealed record CreateComplaintCommand(
        Guid AssociationId,
        string Title,
        string Description,
        string? ImageUrl
) : ICommand<Guid>;
}
