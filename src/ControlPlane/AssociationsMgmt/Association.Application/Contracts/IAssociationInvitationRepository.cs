
using Association.Domain.Entities;
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

    Task SaveChangesAsync(CancellationToken ct = default);
}
