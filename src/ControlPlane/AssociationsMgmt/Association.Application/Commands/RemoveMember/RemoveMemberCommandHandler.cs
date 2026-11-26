using Association.Application.Context;
using Association.Application.Contracts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Commands.RemoveMember;

public sealed class RemoveMemberCommandHandler(
    ICurrentAssociation currentAssociation,
    IAssociationRepository associationRepository)
    : ICommandHandler<RemoveMemberCommand, bool>
{
    public async Task<bool> Handle(
        RemoveMemberCommand command,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(command);

        currentAssociation.EnsureAuthenticated();
        currentAssociation.EnsureAdmin();

        var association = await associationRepository.GetByIdAsync(
            currentAssociation.GetAssociationId(),
            ct) ?? throw new KeyNotFoundException("Association not found.");

        association.RemoveMember(new UserID(command.MemberId));
        await associationRepository.SaveChangesAsync(ct);

        return true;
    }
}
