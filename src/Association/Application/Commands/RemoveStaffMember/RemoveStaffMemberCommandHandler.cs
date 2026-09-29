using Association.Application.Contracts;
using Association.Domain.Associations;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Commands.RemoveStaffMember;

public sealed class RemoveStaffMemberCommandHandler
    : ICommandHandler<RemoveStaffMemberCommand, bool>
{
    private readonly IAssociationRepo _associationRepository;

    public RemoveStaffMemberCommandHandler(
        IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<bool> Handle(
        RemoveStaffMemberCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var AssociationEntity = await _associationRepository.GetByIdAsync(
            new AssociationID(command.AssociationId),
            cancellationToken);

        if (AssociationEntity is null)
        {
            throw new KeyNotFoundException(
                $"AssociationEntity {command.AssociationId} was not found.");
        }

        var member = AssociationEntity.StaffMembers
            .FirstOrDefault(member =>
                member.Id.Value == command.MemberId &&
                member.DeletedAt == null);

        if (member is null)
        {
            throw new KeyNotFoundException(
                $"Member {command.MemberId} was not found.");
        }

        AssociationEntity.RemoveMember(member);

        await _associationRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}


