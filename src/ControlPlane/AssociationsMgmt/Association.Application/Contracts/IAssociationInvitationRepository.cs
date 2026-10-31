
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Association.Application.Contracts;

public interface IAssociationInvitationRepository
{
    Task<bool> IsPendingInvitationExistsAsync(
        AssociationID associationId,
        string email,
        CancellationToken ct = default);

    Task AddAsync(
        AssociationInvitation invitation,
        CancellationToken ct = default);

    Task<AssociationInvitation?> GetByTokenHashAsync(InvitationTokenHash tokenHash, CancellationToken ct = default);

    Task<AssociationInvitation?> GetByIdAsync(InvitationID invitationId, CancellationToken ct = default);

    Task SaveChangesAsync(CancellationToken ct = default);
}
