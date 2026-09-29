using Association.Application.Contracts;
using Association.Domain.Associations;
using Association.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Commands.UpdateStaffMemberRole;

public sealed class UpdateStaffMemberRoleCommandHandler
    : ICommandHandler<UpdateStaffMemberRoleCommand, bool>
{
    private readonly IAssociationRepo _associationRepository;

    public UpdateStaffMemberRoleCommandHandler(
        IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<bool> Handle(
        UpdateStaffMemberRoleCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var AssociationEntity =
            await _associationRepository.GetByIdAsync(
                new AssociationID(command.AssociationId),
                cancellationToken);

        if (AssociationEntity is null)
        {
            throw new KeyNotFoundException(
                $"AssociationEntity {command.AssociationId} was not found.");
        }

        AssociationEntity.UpdateMemberRole(
            new MemberID(command.MemberId),
            command.Role);

        await _associationRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}


