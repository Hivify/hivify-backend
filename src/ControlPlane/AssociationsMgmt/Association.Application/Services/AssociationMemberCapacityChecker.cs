using Association.Application.Contracts;
using SharedKernel.ValueObjects;

namespace Association.Application.Services;

public sealed class AssociationMemberCapacityChecker(
    IAssociationRepository associationRepository,
    IAssociationInvitationRepository invitationRepository,
    IAssociationTierPort associationTierPort)
{
    public async Task EnsureCapacityAvailableAsync(
        AssociationID associationId,
        Guid tierId,
        CancellationToken ct)
    {
        var tierPolicy = await associationTierPort.GetPolicyAsync(
            tierId,
            ct);

        if (tierPolicy is null)
            throw new InvalidOperationException(
                "The Association's tier is unavailable.");

        var memberCount = await associationRepository.GetMemberCountAsync(
            associationId,
            ct);

        var pendingInvitationCount =
            await invitationRepository.GetPendingInvitationCountAsync(
                associationId,
                ct);

        if (memberCount + pendingInvitationCount >= tierPolicy.MaxMembers)
        {
            throw new InvalidOperationException(
                "The Association has reached the maximum member limit for its current tier.");
        }
    }
}